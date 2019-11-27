import { Component, OnInit, Output } from "@angular/core";
import { Subject } from "rxjs";
import { MatSlideToggleChange } from "@angular/material";
import { PlayerService } from "src/app/services/player.service";

@Component({
  selector: "app-sidenav",
  templateUrl: "./sidenav.component.html",
  styleUrls: ["./sidenav.component.css"]
})
export class SidenavComponent implements OnInit {
  @Output() closeNav = new Subject<void>();
  autoPlay: boolean;
  constructor(private player: PlayerService) {}

  ngOnInit() {
    this.autoPlay = this.player.getAutoplay();
  }

  onClose() {
    this.closeNav.next();
  }

  setAutoPlay(e: MatSlideToggleChange) {
    this.player.setAutoPlay(e.checked);
  }
}
