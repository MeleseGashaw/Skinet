import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/Models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-prduct-details',
  templateUrl: './prduct-details.component.html',
  styleUrls: ['./prduct-details.component.scss']
})
export class PrductDetailsComponent implements OnInit {
product: IProduct;
quantity=1;
  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute, 
    private  bcService: BreadcrumbService, private basketService:BasketService) { }

  ngOnInit(): void {
    this.loadProduct();
    }
    addItemToBasket()
    {
      this.basketService.addItemToBasket(this.product,this.quantity);
    }
    increamentQuantity(){
      this.quantity++;
    }
    decreamentQuantity(){
      if(this.quantity>1)
      this.quantity--;
      
    }
loadProduct()
{
  this.shopService.getProduct(+ this.activateRoute.snapshot.paramMap.get('id')).subscribe(product =>{
    this.product=product;
    this.bcService.set('@productDetails',product.name);

  }),error=>{
    console.log(error);
  }
}
}
