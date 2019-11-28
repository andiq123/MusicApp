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
    this.songService.currentSongChanged.subscribe((song: Song) => {
      if (this.song) this.stop();
      this.song = song;
      this.subscriber();
      const name = this.song.name
        ? this.song.artist + "-" + this.song.name
        : this.song.artist;
      this.stream.getSongFromServer(name, this.song.url).subscribe(data => {
        this.newAudio(data);
      });
    });
    this.playerService.playEvent.subscribe(() => this.play());
    this.playerService.pauseEvent.subscribe(() => this.pause());
    this.playerService.stopEvent.subscribe(() => this.stop());
  }

  newAudio(data: Blob) {
    this.player = new Audio(window.URL.createObjectURL(data));
    if (this.player) {
      this.songListener();
      this.play();
    }
  }
  subscriber() {
    this.playerService.autoplayChanged.subscribe(
      (rsp: boolean) => (this.autoPlay = rsp)
    );
    this.songService.currentSongPlaystateUpdated.subscribe(playStatus => {
      this.song.playstatus = playStatus;
    });
  }
  play() {
    this.player.play();
  }

  pause() {
    this.player.pause();
  }

  stop() {
    if (this.song) {
      if (this.song.loading) {
        this.songService.setLoadingState(false, this.song.id);
        this.player = new Audio();
      }

      if (this.song.playstatus == playState.playing) {
        this.player.currentTime = 0;
        this.player.pause();
        this.player = new Audio();
        this.songService.setPlayState(playState.stopped, this.song.id);
      }
    }
  }

  playPrevious() {
    const song = this.song;
    this.stop();
    this.songService.previous(song);
  }

  playNext() {
    const song = this.song;
    this.stop();
    this.songService.next(song);
  }

  setTime(e: MatSlider) {
    this.player.currentTime = e.value;
  }
  setVolume(e: MatSlider) {
    this.volumeSeek = e.value;
  }

  songListener() {
    this.player.onerror = () => {
      this.songService.setError(this.song.id, true);
      this.songService.setLoadingState(false, this.song.id);
      if (this.autoPlay) this.playNext();
    };
    this.player.onplay = () => (this.player.volume = this.volumeSeek * 0.1);
    this.player.onplaying = () => {
      this.duration = this.player.duration;
      this.songService.setPlayState(playState.playing, this.song.id);
      this.songService.setLoadingState(false, this.song.id);
    };
    this.player.onpause = () => {
      if (!this.song.playstatus.stopped)
        this.songService.setPlayState(playState.paused, this.song.id);
    };
    this.player.onended = () => {
      this.player.currentTime = 0;
      this.songService.setPlayState(playState.stopped, this.song.id);
      if (this.autoPlay) this.playNext();
    };
    this.player.ontimeupdate = () => {
      this.player.volume = this.volumeSeek * 0.1;
      this.currentTime = this.player.currentTime;
      this.timeSeek = this.player.currentTime;
    };
  }
}
