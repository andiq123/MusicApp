import { Component, OnInit } from "@angular/core";
import { StreamService } from "src/app/services/stream.service";

@Component({
  selector: "app-connection-check",
  templateUrl: "./connection-check.component.html",
  styleUrls: ["./connection-check.component.css"]
})
export class ConnectionCheckComponent implements OnInit {
  connValid: boolean;
  loading: boolean = false;
  intervalCheck;
  constructor(private stream: StreamService) {}

  ngOnInit() {
    this.stream.connValid.subscribe((rsp: boolean) => {
      clearTimeout(this.intervalCheck);
      this.loading = false;
      this.connValid = rsp;
      if (!this.connValid) {
        this.intervalCheck = setTimeout(() => {
          this.loading = true;
          this.stream.connectionCheck();
        }, 4000);
        clearTimeout(this.intervalCheck);
      }
    });
    this.loading = true;
    this.stream.connectionCheck();
  }
}
