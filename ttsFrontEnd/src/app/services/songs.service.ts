import { Injectable } from "@angular/core";
import { StreamService } from "./stream.service";
import { environment } from "src/environments/environment";
import { Song } from "../models/Song.model";
import { HttpClient } from "@angular/common/http";
import { Subject } from "rxjs";
import { Playstate } from "../models/Playstate.model";
import { playState } from "../utils/playState.utils";

@Injectable({
  providedIn: "root"
})
export class SongsService {
  errorHappened = new Subject<{ song: Song; error: boolean }>();
  errorUpdated = new Subject<string>();
  songsUpdated = new Subject<Song[]>();
  loadingStateUpdate = new Subject<boolean>();
  currentSongPlaystateUpdated = new Subject<Playstate>();
  currentSongChanged = new Subject<Song>();
  backgroundChanged = new Subject<{ url: string; name: string }>();
  songs: Song[];
  currentSong: Song;

  constructor(private stream: StreamService, private http: HttpClient) {}
  searchSongs(text: string) {
    this.songs = null;
    if (!this.stream.development) {
      this.http.get<Song[]>(`${environment.apiUrl}/mixmuz/${text}`).subscribe(
        (songs: Song[]) => {
          if (this.songs) this.songs = [...this.songs, ...songs];
          else this.songs = songs;
          this.songsUpdated.next(this.songs.slice());
        },
        (error: Error) => {
          this.errorUpdated.next(error.message);
        }
      );
      this.http.get<Song[]>(`${environment.apiUrl}/muzfan/${text}`).subscribe(
        (muzfanSongs: Song[]) => {
          if (this.songs) this.songs = [...muzfanSongs, ...this.songs];
          else this.songs = muzfanSongs;
          this.songsUpdated.next(this.songs.slice());
        },
        (error: Error) => {
          this.errorUpdated.next(error.message);
        }
      );
    } else {
      this.songs = [
        {
          album: "adasdasd",
          artist: "boomer",
          cover_art_url:
            "//pic.mixmuz.ru/006366169b27747a781be695eb9b19991b9a1a6a04680d38f23d63033353036bf45dfcecb44d8cc2a4d2e4d2b25d43534d3bdce2b2/Cherocky.jpg",
          name: "miaw epta",
          url: "https://www.bensound.com/bensound-music/bensound-summer.mp3",
          playstatus: playState.stopped
        },
        {
          album: "sadasd",
          artist: "dasder",
          cover_art_url:
            "//pic.mixmuz.ru/00ebe26496e31c59ec5c9959af6e62926292af6864620d38f22d6363035b036bf45dfcecb44d8cc2a4d2e4d2b20d1b534d3bdce2b2/Tommy%20Dotsenko.jpg",
          name: "miaw epsadta",
          url: "https://www.bensound.com/bensound-music/bensound-ukulele.mp3",
          playstatus: playState.stopped
        }
      ];
      this.songsUpdated.next(this.songs);
    }
  }

  previous() {
    if (this.songs) {
      let previous = this.songs[this.songs.indexOf(this.currentSong) - 1];
      if (previous) {
        this.setLoadingState(true, previous);
        this.currentSongChanged.next(previous);
        this.backgroundChanged.next({
          url: previous.cover_art_url,
          name: previous.name
        });
      }
    }
  }

  setCurrentSong(song: Song) {
    this.currentSong = song;
    this.currentSongChanged.next(this.currentSong);
  }

  setLoadingValue(value: number) {
    let song = this.songs.indexOf(this.currentSong);
    this.songs[song].loadingValue = value;
  }

  next() {
    if (this.songs) {
      let next = this.songs[this.songs.indexOf(this.currentSong) + 1];
      if (next) {
        this.setLoadingState(true, next);
        this.currentSongChanged.next(next);
        this.backgroundChanged.next({
          url: next.cover_art_url,
          name: next.name
        });
      }
    }
  }

  setPlayState(playStatus: Playstate) {
    this.currentSong.playstatus = playStatus;
    this.currentSongPlaystateUpdated.next(this.currentSong.playstatus);
  }

  setLoadingState(loading: boolean, song: Song) {
    if (this.songs) {
      const songIndex = this.songs.indexOf(song);
      this.songs[songIndex].loading = loading;
    }
  }

  setError(song: Song, error: boolean) {
    this.errorHappened.next({ song, error });
  }
}

// uploadFromWeb(name: string, link: string) {
//   if (link.includes("mixmuz")) link = link.replace("//", "https://");
//   this.http
//     .post(
//       `${environment.apiUrl}/mixmuz/download`,
//       {
//         name,
//         link
//       },
//       { reportProgress: true, observe: "events" }
//     )
//     .subscribe(event => {
//       if (event.type === HttpEventType.UploadProgress)
//         console.log(Math.round((100 * event.loaded) / event.total));
//       else if (event.type === HttpEventType.Response) {
//         console.log("Upload success.");
//       }
//     });
// }
