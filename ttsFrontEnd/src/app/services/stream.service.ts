import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Subject, Observable } from "rxjs";
import { Song } from "../models/Song.model";
import { HttpEventType, HttpClient, HttpEvent } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class StreamService {
  connValid = new Subject<boolean>();
  development: boolean = false;

  constructor(private http: HttpClient) {}
  getDev() {
    return this.development;
  }

  getSongFromServer(name: string, link: string): Observable<Blob> {
    if (link.includes("mixmuz")) link = link.replace("//", "https://");
    return this.http.post(
      `${environment.apiUrl}/download/song`,
      {
        name,
        link
      },
      { responseType: "blob" }
    );
  }

  uploadFromServer(name: string, link: string) {
    if (link.includes("mixmuz")) link = link.replace("//", "https://");
    this.http
      .post(
        `${environment.apiUrl}/mixmuz/download`,
        {
          name,
          link
        },
        { reportProgress: true, observe: "events" }
      )
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          console.log(Math.round((100 * event.loaded) / event.total));
        else if (event.type === HttpEventType.Response) {
          console.log("Upload success.");
        }
      });
  }

  connectionCheck() {
    this.http.get<Song[]>(`${environment.apiUrl}/mixmuz/connTest`).subscribe(
      () => {
        this.connValid.next(true);
      },
      () => {
        this.connValid.next(false);
      }
    );
  }
}
