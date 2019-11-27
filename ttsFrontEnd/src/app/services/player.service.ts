import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { SongsService } from "./songs.service";

@Injectable({
  providedIn: "root"
})
export class PlayerService {
  playEvent = new Subject<void>();
  pauseEvent = new Subject<void>();
  stopEvent = new Subject<void>();
  autoplay: boolean = false;
  autoplayChanged = new Subject<boolean>();

  constructor(private songService: SongsService) {}

  setAutoPlay(autoPlay: boolean) {
    this.autoplay = autoPlay;
    this.autoplayChanged.next(this.autoplay);
  }

  getAutoplay() {
    return this.autoplay;
  }
  play() {
    this.playEvent.next();
  }

  pause() {
    this.pauseEvent.next();
  }

  stop() {
    this.stopEvent.next();
  }
}
