import {
  Component,
  OnInit,
  Input,
  ElementRef,
  ViewChild,
  AfterViewInit
} from "@angular/core";
import { Song } from "src/app/models/Song.model";
import { SongsService } from "src/app/services/songs.service";
import { PlayerService } from "src/app/services/player.service";
import { StreamService } from "src/app/services/stream.service";
import { saveAs } from "file-saver";

@Component({
  selector: "app-song",
  templateUrl: "./song.component.html",
  styleUrls: ["./song.component.css"]
})
export class SongComponent implements OnInit, AfterViewInit {
  emptyCard: boolean = false;
  @ViewChild("backgroundDiv") backgroundDiv: ElementRef;
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
  ngAfterViewInit(): void {
    this.backgroundDiv.nativeElement.style.backgroundImage = `url(${this.song.cover_art_url})`;
  }

  play() {
    if (this.song.playstatus.paused) this.playerService.play();
    else {
      this.stop();
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

  downloadSong() {
    this.songService.setLoadingState(true, this.song.id);
    const name = this.song.name
      ? this.song.artist + "-" + this.song.name
      : this.song.artist;
    this.stream.getSongFromServer(name, this.song.url).subscribe(data => {
      this.songService.setLoadingState(false, this.song.id);
      saveAs(data, name + ".mp3");
    });
  }
}
