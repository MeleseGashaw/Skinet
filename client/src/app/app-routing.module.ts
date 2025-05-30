import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import { PrductDetailsComponent } from './shop/prduct-details/prduct-details.component';
import { ShopComponent } from './shop/shop.component';
import { skip } from 'rxjs';

const routes: Routes = [
  {path:'',component:HomeComponent, data: {breadcrumb:'Home'}},
  {path:'test-error',component:TestErrorComponent, data: {breadcrumb:'Test Errors'}},
  {path:'server-error',component:ServerErrorComponent, data: {breadcrumb:'Server Errors'}},
  {path:'not-found',component:NotFoundComponent, data: {breadcrumb:'Not Found'}},
  {path:'shop',loadChildren:()=>import('./shop/shop.module').then(mod=>mod.ShopeModule), data: {breadcrumb:'Shop'}},
 // {path:'shop/:id',component:PrductDetailsComponent},
 {path:'basket',loadChildren:()=>import('./basket/basket.module').then(mod=>mod.BasketModule), data: {breadcrumb:'Basket'}},

 {path:'checkout',loadChildren:()=>import('./checkout/checkout.module').then(mod=>mod.CheckoutModule), data: {breadcrumb:'Checkout'}},
 
 {path:'account',loadChildren:()=>import('./account/account.module').then(mod=>mod.AccountModule), data: {skip:true}},
 {path:'**',redirectTo:'not-found', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
