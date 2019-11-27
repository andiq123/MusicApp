import { Component, OnInit, Input } from "@angular/core";
import { Song } from "src/app/models/Song.model";
import { SongsService } from "src/app/services/songs.service";
import { PlayerService } from "src/app/services/player.service";
import { StreamService } from "src/app/services/stream.service";
import { saveAs } from "file-saver";
import { loadingState } from "src/app/utils/loadingState.utils";

@Component({
  selector: "app-song",
  templateUrl: "./song.component.html",
  styleUrls: ["./song.component.css"]
})
export class SongComponent implements OnInit {
  emptyCard: boolean = false;
  @Input() song: Song;
  constructor(
    private songService: SongsService,
    private playerService: PlayerService,
    private stream: StreamService
  ) {}
  ngOnInit() {
    if (this.song.name === "Nothing Found" && !this.song.artist)
      this.emptyCard = true;
  }
  play() {
    this.songService.setLoadingState(true, this.song);
    if (this.song.playstatus.paused) this.playerService.play();
    else {
      this.songService.setCurrentSong(this.song);
    }
  }

  pause() {
    if (this.song.playstatus.playing) this.playerService.pause();
  }

  stop() {
    if (this.song.playstatus.playing || this.song.playstatus.paused)
      this.playerService.stop();
  }
  randomNumber(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
  async createDownloadLink() {
    this.songService.setLoadingState(true, this.song);
    const name = this.song.name
      ? this.song.artist + "-" + this.song.name
      : this.song.artist;
    saveAs(await this.stream.getSongFromServer(name, this.song.url), name);
    this.songService.setLoadingState(false, this.song);
  }
}
