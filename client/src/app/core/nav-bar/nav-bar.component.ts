import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
   
  constructor( ) { }

  ngOnInit(): void { 
  }

}
