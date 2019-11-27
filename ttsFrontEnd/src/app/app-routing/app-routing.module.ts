import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SongsComponent } from "../components/songs/songs.component";
import { AboutComponent } from "../components/about/about.component";
import { NotFoundComponent } from "../components/not-found/not-found.component";

const routes: Routes = [
  { path: "", redirectTo: "/songs", pathMatch: "full" },
  { path: "songs", component: SongsComponent },
  { path: "about", component: AboutComponent },
  { path: "**", component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
