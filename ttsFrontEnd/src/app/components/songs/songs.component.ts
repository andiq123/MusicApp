import { Component, OnInit } from "@angular/core";
import { Song } from "src/app/models/Song.model";
import { SongsService } from "src/app/services/songs.service";
import { StreamService } from "src/app/services/stream.service";

@Component({
  selector: "app-songs",
  templateUrl: "./songs.component.html",
  styleUrls: ["./songs.component.css"]
})
export class SongsComponent implements OnInit {
  loading: boolean = false;
  songs: Song[];
  serverError: boolean = false;
  development: boolean = false;

  constructor(
    private songService: SongsService,
    private stream: StreamService
  ) {}

  ngOnInit() {
    this.songs = this.songService.returnSongs();
    this.development = this.stream.getDev();
    this.songService.songsUpdated.subscribe((songs: Song[]) => {
      this.songs = songs;
      if (this.songs.length == 0) {
        this.songs = [{ id: 0, name: "Nothing Found" }];
      }
      this.loading = false;
    });

    this.songService.errorUpdated.subscribe(
      () => {
        this.serverError = true;
        this.loading = false;
      },
      () => (this.loading = false)
    );

    this.songService.errorHappened.subscribe(
      (data: { song: Song; error: boolean }) =>
        this.setErrorSong(data.song, data.error)
    );
    if (this.development) this.search("test");
  }

  search(searchText: string) {
    this.loading = true;
    this.serverError = false;
    this.songs = null;
    this.songService.searchSongs(searchText);
  }
  setErrorSong(song: Song, error: boolean) {
    let index = this.songs.indexOf(song);
    this.songs[index].error = error;
  }
}
