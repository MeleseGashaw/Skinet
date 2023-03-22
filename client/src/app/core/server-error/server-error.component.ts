import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss']
})
export class ServerErrorComponent implements OnInit {
error: any
  constructor(private router:Router) { 
    const navigation =this.router.getCurrentNavigation();
  this.error= navigation && navigation,Title && navigation.extras.state && navigation.extras.state['error'];
  }

  ngOnInit(): void {
  }

}
