import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { environment } from "src/environments/environment";
import { SongsService } from "./songs.service";

@Injectable({
  providedIn: "root"
})
export class SignalRService {
  public data: Buffer[];
  private hubConnection: signalR.HubConnection;
  constructor(private songService: SongsService) {}
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl.split("api")[0]}buffer`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log("Connection Started"))
      .catch(err => console.log("Error while starting connection: " + err));
  };
  public CommandListener = () => {
    this.hubConnection.on("downStarted", () =>
      console.log("download started yopta!")
    );
    this.hubConnection.on("progressChanged", (progress, bytesRecieved) => {
      this.songService.setLoadingValue(progress);
      if (bytesRecieved > 4990000 && bytesRecieved < 5000000) {
        const blob = new Blob([bytesRecieved], { type: "audio/mpeg" });
        const url = URL.createObjectURL(blob);
        new Audio(url).play();
        console.log(bytesRecieved);
      }
    });
  };
}
