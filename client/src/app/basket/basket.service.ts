import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, IBasket, IBasketItem, IBasketTotals } from '../shared/Models/basket';
import { IProduct } from '../shared/Models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl=environment.apiUrl;
private basketSource=new BehaviorSubject<IBasket>(null);
basket$=this.basketSource.asObservable();
private basketTotalSource=new BehaviorSubject<IBasketTotals>(null);
basketTotal$=this.basketTotalSource.asObservable();
  constructor(private http: HttpClient) { }
  getBasket(id: string)
  {
    return this.http.get(this.baseUrl + 'basket?id=' + id)
    .pipe(
map((
  basket:IBasket
)=>{this.basketSource.next(basket);
  this.calculateTotals();
//console.log(this.getCurrentBasketValue());
})
    );
  }

  setBasket(basket:IBasket){
    return this.http.post(this.baseUrl+'basket',basket).subscribe((response:IBasket)=>{
      this.basketSource.next(response);
      this.calculateTotals();
      //console.log(response);
    }, error => { 
      console.log(error);
    } )
  }
getCurrentBasketValue(){
  return this.basketSource.value;
}

addItemToBasket(item: IProduct, quantity=1){
  const itemToAdd: IBasketItem= this.mapProductItemToBasketItem(item,quantity);
  const basket=this.getCurrentBasketValue()?? this.createBasket();
  basket.items=this.addOrUpdateItem(basket.items,itemToAdd, quantity);
  this.setBasket(basket);
}
increamentItemQuantity(item:IBasketItem){
  const basket=this.getCurrentBasketValue();
  const foundItemIndex=basket.items.findIndex( z=>z.id === item.id);
  basket.items[foundItemIndex].quantity++;
  this.setBasket(basket);

}
decreamentItemQuantity(item:IBasketItem){
  const basket=this.getCurrentBasketValue();
  const foundItemIndex=basket.items.findIndex( z=>z.id === item.id);
  if(basket.items[foundItemIndex].quantity>1){
  basket.items[foundItemIndex].quantity--;
  this.setBasket(basket);
}
  else{
this.removeItemFromBasket(item);
  }
 

}
  removeItemFromBasket(item: IBasketItem) {
    const basket=this.getCurrentBasketValue();
    if(basket.items.some(x=>x.id===item.id))
       basket.items=basket.items.filter(i=>{i.id!==item.id});
if(basket.items.length>0){
  this.setBasket(basket);
}
else{
 this.deleteBasket(basket);
  }
}
  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl+'basket?id='+basket.id).subscribe(()=>{
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
     },error=>{console.log(error);})
    
  }
private calculateTotals(){
const basket=this.getCurrentBasketValue();
const shipping=0;
const subTotal=basket.items.reduce((a,b)=>(b.price*b.quantity)+ a , 0);
const total=subTotal+shipping;
this.basketTotalSource.next({shipping, total, subTotal});
}
 private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
 console.log(items);
  const index=items.findIndex(i=>i.id===itemToAdd.id);
   if (index===-1){
    itemToAdd.quantity=quantity;
    items.push(itemToAdd);
   }else{
    items[index].quantity += quantity;
   }
   return items;
  }
 private createBasket(): IBasket {
    const basket= new Basket();
    localStorage.setItem('basket_id',basket.id);
    return basket;
  }
 private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id:item.id,
      productName:item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.prouctType
    }
  }
}
