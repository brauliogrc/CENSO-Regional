import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SloginService } from '../services/login/slogin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
  */
   loginForm = this._fb.group({
    userName: ['', [Validators.required]],
    password : ['', [Validators.required]]
  });

  constructor(  private _fb:FormBuilder,
                private _loginService:SloginService,
                private router:Router) {
   }

  ngOnInit(): void {
      
  }
  
  userLogin(){
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const dataLogin: any = {
      username: this.loginForm.get('userName')?.value,
      email: this.loginForm.get('password')?.value
    }
    console.log(dataLogin);

    // Nos suscribimos al método del service, enviandole el objeto con los datos para validar si el usuario existe en la base de datos
    this._loginService.Login(dataLogin).subscribe(data => {
      if(!data){
        console.error('Usuario no encontrado en la base de datos');
      }else{
        this.loginForm.reset();
        console.log('Binbenido ' + data.uName);
        this.router.navigate(['/panelusuario']);
      }
      console.log(data);
    }, error => {
      console.error(error);
    });
  }
}
