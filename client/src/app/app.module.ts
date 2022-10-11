import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { core } from '@angular/compiler';
import { CoreModule } from './core/core.module';
import { ShopeModule } from './shop/shop.module';
 
@NgModule({
  declarations: [
    AppComponent
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
   BrowserAnimationsModule,
   HttpClientModule,
   CoreModule,
   ShopeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
