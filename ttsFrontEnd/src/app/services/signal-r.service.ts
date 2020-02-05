import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { environment } from "src/environments/environment";
import { SongsService } from "./songs.service";

@Injectable({
  providedIn: "root"
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  constructor(private songService: SongsService) {}
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl.split("api")[0]}status`)
      .build();

    this.hubConnection
      .start()
      .then(() => {})
      .catch(err => console.log("Error while starting connection: " + err));
  };
  public CommandListener = () => {
    this.hubConnection.on("progressChanged", progress => {
      this.songService.setLoadingValue(progress);
    });
  };
}
