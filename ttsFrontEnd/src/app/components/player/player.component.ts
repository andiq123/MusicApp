import { Component, OnInit } from "@angular/core";
import { Song } from "src/app/models/Song.model";
import { playState } from "src/app/utils/playState.utils";
import { MatSlider } from "@angular/material";
import { PlayerService } from "src/app/services/player.service";
import { SongsService } from "src/app/services/songs.service";
import { StreamService } from "src/app/services/stream.service";

@Component({
  selector: "app-player",
  templateUrl: "./player.component.html",
  styleUrls: ["./player.component.css"]
})
export class PlayerComponent implements OnInit {
  player: HTMLAudioElement;
  error: boolean = false;
  autoPlay: boolean;
  song: Song;
  timeSeek: number;
  currentTime: number;
  duration: number = 0;
  volumeSeek: number = 10;
  constructor(
    private playerService: PlayerService,
    private songService: SongsService,
    private stream: StreamService
  ) {}

  ngOnInit() {
    this.autoPlay = this.playerService.getAutoplay();
    this.playerService.autoplayChanged.subscribe(
      (rsp: boolean) => (this.autoPlay = rsp)
    );
    this.songService.currentSongChanged.subscribe(async (song: Song) => {
      if (this.song) this.stop();
      this.song = song;
      const name = this.song.name
        ? this.song.artist + "-" + this.song.name
        : this.song.artist;
      this.stream.getSongFromServer(name, this.song.url).subscribe(data => {
        this.newAudio(data);
      });
    });

    this.songService.currentSongPlaystateUpdated.subscribe(playStatus => {
      this.song.playstatus = playStatus;
    });
  }

  newAudio(data: Blob) {
    this.player = new Audio(window.URL.createObjectURL(data));
    this.songListener();
    this.play();
    this.playerService.playEvent.subscribe(() => this.play());
    this.playerService.pauseEvent.subscribe(() => this.pause());
    this.playerService.stopEvent.subscribe(() => this.stop());
  }

  play() {
    this.player.play();
  }

  pause() {
    this.player.pause();
  }

  stop() {
    this.player.currentTime = 0;
    this.player.pause();
    this.player = new Audio();
    this.songService.setPlayState(playState.stopped);
  }

  playPrevious() {
    this.songService.previous();
  }

  playNext() {
    this.songService.next();
  }

  setTime(e: MatSlider) {
    this.player.currentTime = e.value;
  }
  setVolume(e: MatSlider) {
    this.volumeSeek = e.value;
  }

  songListener() {
    this.player.onerror = () => {
      this.songService.setError(this.song, true);
      this.songService.setLoadingState(false, this.song);
      if (this.autoPlay) this.playNext();
    };
    this.player.onplay = () => (this.player.volume = this.volumeSeek * 0.1);
    this.player.onplaying = () => {
      this.duration = this.player.duration;
      this.songService.setPlayState(playState.playing);
      this.songService.setLoadingState(false, this.song);
    };
    this.player.onpause = () => {
      if (!this.song.playstatus.stopped)
        this.songService.setPlayState(playState.paused);
    };
    this.player.onended = () => {
      this.player.currentTime = 0;
      this.songService.setPlayState(playState.stopped);
      if (this.autoPlay) this.playNext();
    };
    this.player.ontimeupdate = () => {
      this.player.volume = this.volumeSeek * 0.1;
      this.currentTime = this.player.currentTime;
      this.timeSeek = this.player.currentTime;
    };
  }
}
