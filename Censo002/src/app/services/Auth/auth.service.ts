import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private MyAppUrl: string = 'https://localhost:44358/';
  private MyApiUrl: string = 'api/Login';

  private loggedIn = new BehaviorSubject<boolean>(false);

  get isLogged(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }
  
  constructor(private _http: HttpClient, private router: Router) {
    this.checkToken();
  }

  // Llamamos al controlador del login de la API, le enviamos los datos obtenidos en el formulario de login.component
  Login(authData: any): Observable<any> {
    return this._http.post(this.MyAppUrl + this.MyApiUrl, authData);
  }

  //////////////////////////////////////////////////////////
  // Login(ahutData: any): Observable<any> {
  //   return this._http.post(this.MyAppUrl + this.MyApiUrl, ahutData).pipe(
  //     map((res: any) => {
  //       this.saveId(res.uId);
  //       this.saveName(res.uName);
  //       this.saveRole(res.roleId);
  //       this.saveToken(res.token);
  //       this.loggedIn.next(true);
  //       return res;
  //     })
  //   );
  // }
  //////////////////////////////////////////////////////////

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  /** METODOS PARA LA AUTENTICACIÓN */

  logout(): void {
    sessionStorage.clear();
    console.clear();
    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }

  private checkToken(): void {
    const userToken: any = sessionStorage.getItem('token');
    const isExpired = helper.isTokenExpired(userToken);
    console.log('is Expired: ', isExpired);
    isExpired ? this.logout() : this.loggedIn.next(true);
  }

  // Guardado de datos en el sessionSorage
  saveEmployeenumber(employeeNumber: any): void {
    sessionStorage.setItem('employeeNumber', employeeNumber);
  }

  saveUserId(id: any): void {
    sessionStorage.setItem('userId', id);
  }

  saveUsername(username: string): void {
    sessionStorage.setItem('username', username);
  }

  saveSupervisorNumber(supervisorNumber: any): void {
    sessionStorage.setItem('supervisorNumber', supervisorNumber);
  }

  saveLocation(location: any): void {
    sessionStorage.setItem('location', location);
  }

  saveRole(role: any): void {
    sessionStorage.setItem('role', role);
  }

  saveEmail(email: string): void {
    sessionStorage.setItem('email', email);
  }

  saveToken(token: string): void {
    sessionStorage.setItem('token', token);
  }
}
