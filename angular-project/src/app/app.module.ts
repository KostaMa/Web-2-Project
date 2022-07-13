import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationComponent } from './routing/navigation/navigation.component';
import { PersonListComponent } from './person/person-list/person-list.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './person/login/login.component';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PersonDetailsComponent } from './person/person-details/person-details.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductListComponent } from './product/product-list/product-list.component';
import { HomeComponent } from './home/home.component';
import { ProductUpdateComponent } from './product/product-update/product-update.component';
import { OrderNewComponent } from './order/order-new/order-new.component';
import { OrderCurrentComponent } from './order/order-current/order-current.component';
import { OrderHistoryComponent } from './order/order-history/order-history.component';
import { ProductNewComponent } from './product/product-new/product-new.component';
import { PersonVerificationComponent } from './person/person-verification/person-verification.component';
import { OrderListComponent } from './order/order-list/order-list.component';
import { MyOrderListComponent } from './order/my-order-list/my-order-list.component';
import { RegisterComponent } from './person/register/register.component';
import { CountdownComponent } from './countdown/countdown.component';
import { SocialLoginModule } from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';
import { JwtModule } from "@auth0/angular-jwt";
import { environment } from 'src/environments/environment';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    PersonListComponent,
    LoginComponent,
    PersonDetailsComponent,
    ProductListComponent,
    HomeComponent,
    ProductUpdateComponent,
    OrderNewComponent,
    OrderCurrentComponent,
    OrderHistoryComponent,
    ProductNewComponent,
    PersonVerificationComponent,
    OrderListComponent,
    MyOrderListComponent,
    RegisterComponent,
    CountdownComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    SocialLoginModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains:environment.allowedDomains
      }
      }),
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true, //keeps the user signed in
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('148517665605-jspahbqleats6lvlag9kasc2c11b5g7o.apps.googleusercontent.com')

          }
        ]
      }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
