import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Subject, Observable } from "rxjs";
import { Song } from "../models/Song.model";
import { HttpClient } from "@angular/common/http";

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
