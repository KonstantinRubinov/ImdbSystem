import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ImdbComponent } from '../imdb/imdb.component';


const routes: Routes = [
  {
    path: '',
    component: ImdbComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ImdbRoutingModule { }
