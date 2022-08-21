import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MainComponent } from './pages/main/main.component';
import { AuthorizeComponent } from './pages/authorize/authorize.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationComponent } from './components/navigation/navigation.component';
import { AdvertListComponent } from './pages/advert-list/advert-list.component';
import { AdvertComponent } from './components/advert/advert.component';

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
    AdvertComponent
  ],
  imports: [
    BrowserModule,
    MatSidenavModule,
    FormsModule,
    AppRoutingModule,
    RouterModule.forRoot([
      { path: '', component: MainComponent },
      { path: 'authorize', component: AuthorizeComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'advert-list', component: AdvertListComponent }
    ]),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
