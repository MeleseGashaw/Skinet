import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { PrductDetailsComponent } from './prduct-details/prduct-details.component';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes=[
  {path:'',component:ShopComponent},
  {path:':id',component:PrductDetailsComponent, data:{breadcrumb: {alias: 'productDetails'}}},
];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class ShopRoutingModule { }
