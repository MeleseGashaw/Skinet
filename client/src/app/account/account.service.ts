import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Iuser } from '../shared/Models/user';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl=environment.apiUrl;
private currentUserSource=new ReplaySubject<Iuser>(1);
currentUser$=this.currentUserSource.asObservable();
  constructor( private http: HttpClient,private router:Router) { }

 
  
  loadCurrentUser(token:string){
    if(token===null)
    {
      this.currentUserSource.next(null);
      return of(null);
    }
let headers=new HttpHeaders();
headers=headers.set('Authorization', `Bearer ${token}`);
return this.http.get(this.baseUrl + 'account', {headers}).pipe(
  map ((user:Iuser)=> {
    if(user){
      localStorage.setItem('token',user.token)
      this.currentUserSource.next(user);
    }
  })
);
  }

  login(values:any){
    return this.http.post(this.baseUrl +'account/login',values).pipe(
      map((user:Iuser) =>{
      if(user)
      {
        localStorage.setItem('token',user.token);
        this.currentUserSource.next(user);
      }
    })
    );
  }
  register(values:any){
    return this.http.post(this.baseUrl +'account/register',values).pipe(
      map((user:Iuser) =>{
      if(user)
      {
        localStorage.setItem('token',user.token);
        this.currentUserSource.next(user);
      }
    })
    );
  }
  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
  checkEmailExists(email: string){
    return this.http.get(this.baseUrl+'account/emailexists?email='+email)
  }
}
