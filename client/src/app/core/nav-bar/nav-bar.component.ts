import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/Models/basket';
import { Iuser } from 'src/app/shared/Models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
   basket$: Observable<IBasket>;
   currentUser$:Observable<Iuser>;
  constructor(private basketService: BasketService , private accountService: AccountService) { }

  ngOnInit(): void { 
    this.basket$=this.basketService.basket$;
    this.currentUser$=this.accountService.currentUser$;
  }
  Logout(){
    this.accountService.logout();
  }
}
