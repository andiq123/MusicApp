import { Component, OnInit } from "@angular/core";
import { SignalRService } from "./services/signal-r.service";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit {
  loading: boolean = false;
  constructor(
    private signalRService: SignalRService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.CommandListener();
    this.startHttpRequest();
  }
  startHttpRequest() {
    this.http
      .get(`${environment.apiUrl.split("api")[0]}buffer`)
      .subscribe(res => console.log(res));
  }
}
