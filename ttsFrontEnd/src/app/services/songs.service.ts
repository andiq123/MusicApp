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
  loadingSong = new Subject<boolean>();
  currentSongPlaystateUpdated = new Subject<Playstate>();
  currentSongChanged = new Subject<Song>();
  backgroundChanged = new Subject<{ url: string; name: string }>();
  songs: Song[];
  index: number;

  constructor(private stream: StreamService, private http: HttpClient) {}
  searchSongs(text: string) {
    this.index = 0;
    this.songs = null;
    if (!this.stream.development) {
      this.getMixMuzSongs(text);
      this.getMuzFanSongs(text);
    } else {
      this.songs = [
        {
          id: 0,
          album: "adasdasd",
          artist: "boomer",
          cover_art_url:
            "//pic.mixmuz.ru/006366169b27747a781be695eb9b19991b9a1a6a04680d38f23d63033353036bf45dfcecb44d8cc2a4d2e4d2b25d43534d3bdce2b2/Cherocky.jpg",
          name: "miaw epta",
          url: "https://www.bensound.com/bensound-music/bensound-summer.mp3",
          playstatus: playState.stopped
        },
        {
          id: 1,
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

  getMuzFanSongs(text: string) {
    this.http.get<Song[]>(`${environment.apiUrl}/muzfan/${text}`).subscribe(
      (muzfanSongs: Song[]) => {
        if (this.songs) this.songs = [...this.songs, ...muzfanSongs];
        else this.songs = muzfanSongs;
        this.index++;
        this.indexSongsAndAlert();
      },
      (error: Error) => {
        this.errorUpdated.next(error.message);
      }
    );
  }

  getMixMuzSongs(text: string) {
    this.http.get<Song[]>(`${environment.apiUrl}/mixmuz/${text}`).subscribe(
      (songs: Song[]) => {
        if (this.songs) this.songs = [...this.songs, ...songs];
        else this.songs = songs;
        this.index++;
        this.indexSongsAndAlert();
      },
      (error: Error) => {
        this.errorUpdated.next(error.message);
      }
    );
  }

  returnSongs() {
    if (this.songs) return this.songs;
  }
  indexSongsAndAlert() {
    this.songs.forEach(x => (x.id = this.index++));
    this.songsUpdated.next(this.songs.slice());
    if (this.index > 1) this.loadingSong.next(false);
  }

  previous(song: Song) {
    let previous = this.songs[this.songs.indexOf(song) - 1];
    if (previous) {
      this.setLoadingState(true, previous.id);
      this.currentSongChanged.next(previous);
      this.backgroundChanged.next({
        url: previous.cover_art_url,
        name: previous.artist
      });
    }
  }

  setCurrentSong(song: Song) {
    let anyLoading;
    if (this.songs) anyLoading = this.songs.find(x => x.loading == true);
    if (!anyLoading) {
      this.setLoadingState(true, song.id);
      this.currentSongChanged.next(song);
      this.backgroundChanged.next({
        url: song.cover_art_url,
        name: song.artist
      });
    }
  }

  setLoadingValue(value: number) {
    let song = this.songs.find(x => x.loading == true);
    if (song) song.loadingValue = value;
  }

  next(song: Song) {
    let next = this.songs[this.songs.indexOf(song) + 1];
    if (next) {
      this.setLoadingState(true, next.id);
      this.currentSongChanged.next(next);
      this.backgroundChanged.next({
        url: next.cover_art_url,
        name: next.artist
      });
    }
  }

  setPlayState(playStatus: Playstate, id: number) {
    const song = this.songs.find(x => x.id === id);
    if (song) {
      song.playstatus = playStatus;
      this.currentSongPlaystateUpdated.next(song.playstatus);
    }
  }

  setLoadingState(loading: boolean, id: number) {
    const song = this.songs.find(x => x.id === id);
    if (song) song.loading = loading;
  }

  setError(id: number, error: boolean) {
    const song = this.songs.find(x => x.id === id);
    if (song) this.errorHappened.next({ song, error });
  }
}
