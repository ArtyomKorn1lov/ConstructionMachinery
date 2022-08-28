import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
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
import { LeaseRegistrationComponent } from './pages/lease-registration/lease-registration.component';
import { ConfirmListComponent } from './pages/confirm-list/confirm-list.component';
import { RequestComponent } from './components/request/request.component';
import { MyRequestsComponent } from './pages/my-requests/my-requests.component';
import { ConfirmListInfoComponent } from './pages/confirm-list-info/confirm-list-info.component';
import { MyRequestInfoComponent } from './pages/my-request-info/my-request-info.component';
import { EditProfileComponent } from './pages/edit-profile/edit-profile.component';

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
    LeaseRegistrationComponent,
    ConfirmListComponent,
    RequestComponent,
    MyRequestsComponent,
    ConfirmListInfoComponent,
    MyRequestInfoComponent,
    EditProfileComponent,
  ],
  imports: [
    BrowserModule,
    MatSidenavModule,
    MatDatepickerModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    HttpClientModule,
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
      { path: 'my-adverts', component: MyAdvertsComponent },
      { path: 'lease-registration', component: LeaseRegistrationComponent },
      { path: 'confirm-list', component: ConfirmListComponent },
      { path: 'my-requests', component: MyRequestsComponent },
      { path: 'confirm-list/info', component: ConfirmListInfoComponent },
      { path: 'my-requests/info', component: MyRequestInfoComponent},
      { path: 'profile/edit', component: EditProfileComponent }
    ]),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
