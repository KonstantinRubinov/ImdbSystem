import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginGuardService } from '../services/login-guard.service';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  { path: "imdb",
  loadChildren: () => import('../components/data-elements/Imdb/imdb.module').then(m => m.ImdbModule) },
  { path: "favorites",canActivate: [LoginGuardService],
  loadChildren: () => import('../components/data-elements/favorites/favorites.module').then(m => m.FavoritesModule) },
  { path: "movieDetails/:id", canActivate: [LoginGuardService],
  loadChildren: () => import('../components/data-elements/movie-details/movie-details.module').then(m => m.MovieDetailsModule) },
  { path: '', redirectTo: "imdb", pathMatch: "full" },
];

@NgModule({
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
