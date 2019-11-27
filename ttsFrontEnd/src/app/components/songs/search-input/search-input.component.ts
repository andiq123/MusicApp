import { Component, OnInit, Output } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Subject } from "rxjs";
import {
  MatSlideToggle,
  MatButtonToggle,
  MatSlideToggleChange
} from "@angular/material";
import { PlayerService } from "src/app/services/player.service";

@Component({
  selector: "app-search-input",
  templateUrl: "./search-input.component.html",
  styleUrls: ["./search-input.component.css"]
})
export class SearchInputComponent implements OnInit {
  @Output() searchStart = new Subject<string>();
  autoPlay: boolean;
  constructor(private player: PlayerService) {}

  ngOnInit() {
    this.autoPlay = this.player.getAutoplay();
  }

  onSubmit(form: NgForm) {
    if (form.valid) this.searchStart.next(form.value.search);
  }

  setAutoPlay(e: MatSlideToggleChange) {
    this.player.setAutoPlay(e.checked);
  }
}
