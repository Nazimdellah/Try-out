import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiBaseUrl = "https://localhost:7179/api/"
  userBaseUrl = this.apiBaseUrl + "Users/";

  username = "test";
  motDePasse = "Passw0rd!";

  constructor(public http: HttpClient) { }

  async register(email: string, password: string, passwordConfirm: string): Promise<any> {
    let registerData = {

        email: email,
        password: password,
        passwordConfirm: passwordConfirm
    }
    let x = await lastValueFrom(this.http.post<any>(this.userBaseUrl + 'Register', registerData));

    console.log(x);
    return x
  }

  async login(Email: any, Password: any) {
    let registerData = {
        email: Email,
        password: Password
    }
    let result = await lastValueFrom(this.http.post<any>(this.userBaseUrl + 'Login', registerData));

    console.log(result);
    sessionStorage.setItem("token", result.token);
    sessionStorage.setItem('username', Email);
    sessionStorage.setItem('playerId', result.playerId);
    sessionStorage.setItem('playerNumId', result.playerNumId)
    return result;
  }

  async publicCall(): Promise<string[]> {
    return await lastValueFrom(this.http.get<any>(this.userBaseUrl + 'PublicTest'));
  }

  async privateCall(): Promise<string[]> {
    return await lastValueFrom(this.http.get<any>(this.userBaseUrl + 'PrivateTest'));
  }

  async privateCall2(): Promise<string[]> {
    return await lastValueFrom(this.http.get<any>(this.userBaseUrl + 'PrivateData'));
  }

  async logout() {
    sessionStorage.removeItem('username');
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('playerId');
    sessionStorage.removeItem('playerNumId');
    sessionStorage.removeItem('joinMatchData');
  }

  isLoggedIn(): Boolean {
    return sessionStorage.getItem('token') != null;
  }
}
