 
import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountRoutingModule } from './account/account-routing.module';
import { AccountModule } from './account/account.module';
import { AccountService } from './account/account.service';
 
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  
  title = 'Skinet';
 
  constructor( private basketService: BasketService , private accountService:AccountService) { }

  ngOnInit(): void {
   this. loadBasket();
   this.loadCurrentUser();
  }

  loadCurrentUser(){
    const token=localStorage.getItem('token');
    if(token){
      this.accountService.loadCurrentUser(token).subscribe(()=>{
        console.log('loaded user');
       
      },error=>{console.log(error);
    })
    }
  }

  loadBasket(){
    const basketId=localStorage.getItem('basket_id');
    if(basketId){
      this.basketService.getBasket(basketId).subscribe(()=>{
        console.log('initialised basket');
      },error=>{console.log(error);
    });
    }
  }
}
