import { NgModule } from '@angular/core';
import { MovieDetailsRoutingModule } from '../movie-details/movie-details-routing.module';
import { MovieDetailsComponent } from '../movie-details/movie-details.component';
import { MainModule } from 'src/app/modules/main.module';

@NgModule({
  imports: [
    MovieDetailsRoutingModule,
    MainModule
  ],
  declarations: [MovieDetailsComponent]
})
export class MovieDetailsModule { }
