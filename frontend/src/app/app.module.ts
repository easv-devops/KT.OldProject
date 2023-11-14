import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { ReactiveFormsModule } from "@angular/forms";

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { ErrorHttpInterceptor } from 'src/interceptors/error-http-interceptor';
import { TokenService } from 'src/services/token.service';
import { AuthHttpInterceptor } from 'src/interceptors/auth-http-interceptor';
import { NgModule } from '@angular/core';
import { TabsComponent } from './tabs.component';
import { HeaderComponent } from './header.component';
import { RegisterComponent } from './account/register.component';
import { LoginComponent } from './account/login.component';
import { AccountComponent } from './account/account.component';
import { HomeComponent } from './home.component';
import { UsersComponent } from './admin/users.component';
import { AuthenticatedGuard } from './guards';
import { AccountService } from './account/account.service';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TabsComponent,
    HomeComponent,
    UsersComponent,
    AccountComponent,
    RegisterComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot({ mode: 'ios' }),
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHttpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true },
    TokenService,
    AuthenticatedGuard,
    AccountService,

  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
