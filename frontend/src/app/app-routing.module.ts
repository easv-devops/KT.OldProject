import {NgModule} from '@angular/core';
import {PreloadAllModules, RouterModule, Routes} from '@angular/router';
import {TabsComponent} from './tabs.component';
import {ProductsComponent} from './products/products.component';
import {AccountComponent} from './account/account.component';
import {LoginComponent} from './account/login.component';
import {RegisterComponent} from './account/register.component';
import {UsersComponent} from './admin/users.component';
import {AuthenticatedGuard} from './guards';
import { CartComponent } from './cart/cart.component';
import {CheckoutComponent} from "./checkout/checkout.component";

const routes: Routes = [
  {


    path: '',
    component: TabsComponent,
    children: [
      {
        path: 'products',
        component: ProductsComponent,
        canActivate: [AuthenticatedGuard]
      },

      {
        path: 'account',
        component: AccountComponent,
        canActivate: [AuthenticatedGuard]
      },
      {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: 'register',
        component: RegisterComponent,
      },
      {
        path: 'users',
        component: UsersComponent,
        canActivate: [AuthenticatedGuard]
      },
      {
        path: 'cart',
        component: CartComponent,
        canActivate: [AuthenticatedGuard]
      },

      {
        path: 'checkout',
        component: CheckoutComponent,
        canActivate: [AuthenticatedGuard]
      },

    ]

  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {preloadingStrategy: PreloadAllModules})
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
