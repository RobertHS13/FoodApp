import { Inject, Injectable } from '@angular/core';
import { User } from '../interfaces';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService{

  user: User;

  baseUrl = "https://localhost:44365/api/User";

  constructor(private http: HttpClient) { 
  }

  public registerUser(user: User){
    this.http.post<User>(`${this.baseUrl}`, user).subscribe(result =>{
      this.user = result;
      console.log("Thisss: "+this.user.firstName);
      console.log("Thisss: "+this.user.lastName);
      console.log("Thisss: "+this.user.type);
      console.log("Thisss: "+this.user.phone);
      console.log("Thisss: "+this.user.email);
      console.log("Thisss: "+this.user.password);
    });
  }

  public loginUser(email: string, password: string){
    this.http.post<User>(`${this.baseUrl + "/Login"}`, {
      "email": email,
      "password": password
    }).subscribe(result => {
      this.user = result;
      console.log("Thisss: "+this.user.firstName);
      console.log("Thisss: "+this.user.lastName);
      console.log("Thisss: "+this.user.type);
      console.log("Thisss: "+this.user.phone);
      console.log("Thisss: "+this.user.email);
      console.log("Thisss: "+this.user.password);
    });
  }
}
