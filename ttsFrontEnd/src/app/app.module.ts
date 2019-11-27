import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AboutComponent } from "./components/about/about.component";
import { HeaderComponent } from "./components/header/header.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { SongsComponent } from "./components/songs/songs.component";
import { SongComponent } from "./components/songs/song/song.component";
import { AppRoutingModule } from "./app-routing/app-routing.module";
import { MaterialComponentsModule } from "./material-components/material-components.module";
import { FlexLayoutModule } from "@angular/flex-layout";
import { SearchInputComponent } from "./components/songs/search-input/search-input.component";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { PlayerComponent } from "./components/player/player.component";
import { SidenavComponent } from "./components/sidenav/sidenav.component";
import { ConnectionCheckComponent } from "./components/connection-check/connection-check.component";
import { BackgroundCoverComponent } from "./components/background-cover/background-cover.component";
import { ChartsModule } from "ng2-charts";

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    HeaderComponent,
    NotFoundComponent,
    PlayerComponent,
    SongsComponent,
    SongComponent,
    SearchInputComponent,
    SidenavComponent,
    ConnectionCheckComponent,
    BackgroundCoverComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MaterialComponentsModule,
    FlexLayoutModule,
    FormsModule,
    HttpClientModule,
    ChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
