import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup,Validator, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
loginForm: FormGroup;
returnurl:string;
  constructor(private accountService:AccountService,private router:Router,private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.returnurl=this.activatedRoute.snapshot.queryParams['returnurl'] || '/shop';
    this.createLoginForm();
  }
createLoginForm(){
  this.loginForm=new FormGroup({
    email: new FormControl('',[Validators.required,Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
   
    password: new FormControl('',Validators.required)
  })
}
onSubmit(){
 // console.log(this.loginForm.value);
 this.accountService.login(this.loginForm.value).subscribe(()=>{
  //console.log('user logged in');
  this.router .navigateByUrl(this.returnurl)
 }, error => {
  console.log(error);
 });
}
}
