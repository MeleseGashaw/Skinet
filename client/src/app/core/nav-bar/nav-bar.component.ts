import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/Models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
   basket$: Observable<IBasket>;
  constructor(private basketService: BasketService ) { }

  ngOnInit(): void { 
    this.basket$=this.basketService.basket$;
  }

}
