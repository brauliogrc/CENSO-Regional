import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { dataLogin, Token } from '../interfaces/newInterfaces';

const helper = new JwtHelperService();

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
   */
  loginForm = this._fb.group({
    usernumber: ['', [Validators.required]],
    password: ['', [Validators.required]],
  });

  token: any;
  dataToken: any;

  constructor(
    private _fb: FormBuilder,
    private _authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  userLogin() {
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const dataLogin: dataLogin = {
      usernumber: this.loginForm.get('usernumber')?.value,
      pass: this.loginForm.get('password')?.value,
    };
    console.log(dataLogin);

    // Nos suscribimos al método del service, enviandole el objeto con los datos para validar si el usuario existe en la base de datos
    this._authService.Login(dataLogin).subscribe(
      (data: Token) => {
        if (!data) {
          console.error('Usuario no encontrado en la base de datos');
        } else {
          this.dataToken = helper.decodeToken(data.token);
          console.log(this.dataToken);

          // this.loginForm.reset();

          // Llamadas a los métodos de guardado del sessionStorage
          this._authService.saveEmployeenumber(this.dataToken.nameid);
          // this._authService.saveUserId(this.dataToken.userId);
          this._authService.saveUsername(this.dataToken.Username);
          this._authService.saveSupervisorNumber(
            this.dataToken.SupervisorNumber
          );
          this._authService.saveLocation(this.dataToken.Location);
          this._authService.saveRole(this.dataToken.Role);
          this._authService.saveEmail(this.dataToken.Email);
          this._authService.saveToken(data.token);

          data.token = '';
          this.dataToken = null;
          this.redirect();

          // alert('Binbenido ' + this.dataToken.Username);
        }
        // console.log(dataLogin);
      },
      (error) => {
        console.error(error.error);
      }
    );
  }

  redirect() {
    const role: string | null = sessionStorage.getItem('role');

    let aut: boolean = role == 'undefined' ? false : true;

    if (aut && this._authService.isLogged) {
      this.router.navigate(['/paneladmin']);
    } else if (this._authService.isLogged) {
      this.router.navigate(['/panelusuario']);
    }
  }
}
