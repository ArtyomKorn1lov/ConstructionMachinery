import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormField } from '@angular/material/form-field';
import { MatHint } from '@angular/material/form-field';
import { MatLabel } from '@angular/material/form-field';
import { MatError } from '@angular/material/form-field';
import { MainComponent } from './pages/main/main.component';
import { AuthorizeComponent } from './pages/authorize/authorize.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationComponent } from './components/navigation/navigation.component';
import { AdvertListComponent } from './pages/advert-list/advert-list.component';
import { AdvertComponent } from './components/advert/advert.component';
import { AdvertCreateComponent } from './pages/advert-create/advert-create.component';
import { AdvertCreateTimeComponent } from './pages/advert-create-time/advert-create-time.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { PrivateAreaComponent } from './pages/private-area/private-area.component';
import { AdvertInfoComponent } from './pages/advert-info/advert-info.component';
import { MyAdvertsComponent } from './pages/my-adverts/my-adverts.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    MainComponent,
    AuthorizeComponent,
    RegistrationComponent,
    NavigationComponent,
    AdvertListComponent,
    AdvertComponent,
    AdvertCreateComponent,
    AdvertCreateTimeComponent,
    ProfileComponent,
    PrivateAreaComponent,
    AdvertInfoComponent,
    MyAdvertsComponent,
  ],
  imports: [
    BrowserModule,
    MatSidenavModule,
    MatDatepickerModule,
    FormsModule,
    AppRoutingModule,
    RouterModule.forRoot([
      { path: '', component: MainComponent },
      { path: 'authorize', component: AuthorizeComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'advert-list', component: AdvertListComponent },
      { path: 'advert-create', component: AdvertCreateComponent },
      { path: 'advert-create/time', component: AdvertCreateTimeComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'private-area', component: PrivateAreaComponent },
      { path: 'advert-info', component: AdvertInfoComponent },
      { path: 'my-adverts', component: MyAdvertsComponent }
    ]),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
