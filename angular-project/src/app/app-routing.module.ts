import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from './auth/admin.guard';
import { AuthGuard } from './auth/auth.guard';
import { DelivererGuard } from './auth/deliverer.guard';
import { LoggedInGuard } from './auth/logged-in.guard';
import { LoginGuard } from './auth/login.guard';
import { UserGuard } from './auth/user.guard';
import { CountdownComponent } from './countdown/countdown.component';
import { HomeComponent } from './home/home.component';
import { MyOrderListComponent } from './order/my-order-list/my-order-list.component';
import { OrderHistoryComponent } from './order/order-history/order-history.component';
import { OrderListComponent } from './order/order-list/order-list.component';
import { OrderNewComponent } from './order/order-new/order-new.component';
import { LoginComponent } from './person/login/login.component';
import { PersonDetailsComponent } from './person/person-details/person-details.component';
import { PersonVerificationComponent } from './person/person-verification/person-verification.component';
import { RegisterComponent } from './person/register/register.component';
import { ProductListComponent } from './product/product-list/product-list.component';
import { ProductNewComponent } from './product/product-new/product-new.component';

const routes: Routes = [
  { path: '', component: LoginComponent, canActivate: [LoginGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [LoginGuard]},
  { path: 'home', component: HomeComponent, 
    children: [ { path: 'persons/:id/details', component: PersonDetailsComponent, canActivate: [LoggedInGuard]},
                { path: 'new-order', component: OrderNewComponent, canActivate: [UserGuard]},
                { path: 'countdown', component: CountdownComponent, canActivate: [LoggedInGuard]},
                { path: 'history', component: OrderHistoryComponent, canActivate: [UserGuard]},
                { path: 'verification', component: PersonVerificationComponent, canActivate: [AdminGuard]},
                { path: 'orders', component: OrderListComponent, canActivate: [AdminGuard]},        //admin
                { path: 'new-product', component: ProductNewComponent, canActivate: [AdminGuard]},
                { path: 'my-orders', component: MyOrderListComponent, canActivate: [DelivererGuard]},
                { path: 'new-orders', component: OrderListComponent, canActivate: [DelivererGuard]}    //deliverer
    ]
  },
  { path: 'product', component: ProductListComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
