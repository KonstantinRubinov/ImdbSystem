import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ImageCropperModule } from 'ngx-image-cropper';
import { CookieService } from 'ngx-cookie-service';

import { Store } from './redux/store';
import { NgReduxModule, NgRedux } from 'ng2-redux';
import { Reducer } from './redux/reducer';

import { MenuComponent } from './components/design-elements/menu/menu.component';
import { LayoutComponent } from './components/design-elements/layout/layout.component';
import { HeaderComponent } from './components/design-elements/header/header.component';
import { FooterComponent } from './components/design-elements/footer/footer.component';
import { SignUpComponent } from './components/user-elements/sign-up/sign-up.component';
import { SignInComponent } from './components/user-elements/sign-in/sign-in.component';
import { MainModule } from './modules/main.module';
import { AppRoutingModule } from './modules/app-routing.module';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [
    MenuComponent,
    LayoutComponent,
    HeaderComponent,
    FooterComponent,
    SignUpComponent,
    SignInComponent
  ],
  imports: [
    BrowserModule,
    NgReduxModule,
    HttpClientModule,
    ImageCropperModule,
    AppRoutingModule,
    MainModule
  ],
  providers: [CookieService ],
  bootstrap: [LayoutComponent]
})
export class AppModule { 
  public constructor(redux:NgRedux<Store>){
    redux.configureStore(Reducer.reduce, new Store());
  }
}