import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from "@auth0/angular-jwt";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
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
import { MyAdvertsRequestsComponent } from './pages/my-adverts-requests/my-adverts-requests.component';
import { MyAdvertsConfirmListComponent } from './pages/my-adverts-confirm-list/my-adverts-confirm-list.component';
import { AdvertRequestComponent } from './components/advert-request/advert-request.component';
import { AdvertEditComponent } from './pages/advert-edit/advert-edit.component';
import { AdvertEditTimeComponent } from './pages/advert-edit-time/advert-edit-time.component';
import { GalleryComponent } from './components/gallery/gallery.component';
import { AuthGuard } from './guards/auth.guards';

export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}

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
    MyAdvertsRequestsComponent,
    MyAdvertsConfirmListComponent,
    AdvertRequestComponent,
    AdvertEditComponent,
    AdvertEditTimeComponent,
    GalleryComponent,
  ],
  imports: [
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes: []
      }
    }),
    BrowserModule,
    MatSidenavModule,
    MatDatepickerModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatProgressSpinnerModule,
    NgxMaterialTimepickerModule,
    HttpClientModule,
    AppRoutingModule,
    RouterModule.forRoot([
      { path: '', component: MainComponent },
      { path: 'authorize', component: AuthorizeComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'advert-list', component: AdvertListComponent },
      { path: 'advert-create', component: AdvertCreateComponent, canActivate: [AuthGuard] },
      { path: 'advert-create/time', component: AdvertCreateTimeComponent, canActivate: [AuthGuard] },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
      { path: 'private-area', component: PrivateAreaComponent, canActivate: [AuthGuard] },
      { path: 'advert-info', component: AdvertInfoComponent },
      { path: 'my-adverts', component: MyAdvertsComponent, canActivate: [AuthGuard] },
      { path: 'lease-registration', component: LeaseRegistrationComponent, canActivate: [AuthGuard] },
      { path: 'advert-confirm/confirm-list', component: ConfirmListComponent, canActivate: [AuthGuard] },
      { path: 'advert-request/my-requests', component: MyRequestsComponent, canActivate: [AuthGuard] },
      { path: 'advert-confirm/confirm-list/info', component: ConfirmListInfoComponent, canActivate: [AuthGuard] },
      { path: 'advert-request/my-requests/info', component: MyRequestInfoComponent, canActivate: [AuthGuard] },
      { path: 'profile/edit', component: EditProfileComponent, canActivate: [AuthGuard] },
      { path: 'advert-confirm', component: MyAdvertsConfirmListComponent, canActivate: [AuthGuard] },
      { path: 'advert-request', component: MyAdvertsRequestsComponent, canActivate: [AuthGuard] },
      { path: 'advert-edit', component: AdvertEditComponent, canActivate: [AuthGuard] },
      { path: 'advert-edit/time', component: AdvertEditTimeComponent, canActivate: [AuthGuard] }
    ]),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
