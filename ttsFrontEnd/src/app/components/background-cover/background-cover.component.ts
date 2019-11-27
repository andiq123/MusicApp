import { Component, OnInit } from "@angular/core";
import { SongsService } from "src/app/services/songs.service";

@Component({
  selector: "app-background-cover",
  templateUrl: "./background-cover.component.html",
  styleUrls: ["./background-cover.component.css"]
})
export class BackgroundCoverComponent implements OnInit {
  backgroundUrl: string;
  name: string;

  constructor(private songService: SongsService) {}

  ngOnInit() {
    this.songService.backgroundChanged.subscribe(
      (data: { url: string; name: string }) => {
        this.backgroundUrl = data.url;
        this.name = data.name;
      }
    );
  }

  setBackground(url: string) {
    this.backgroundUrl = url;
  }
}
