import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SloginService } from '../services/login/slogin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form : FormGroup;

  constructor( private fb:FormBuilder, private _loginService:SloginService) {
    this.form = this.fb.group({
      userName: [''],
      password : ['']
    });
   }

  ngOnInit(): void {
      
  }

  /*UserLogin(){
    console.log(this.form);
    
    const user : any = {
      userName: this.form.get('userName')?.value,
      password: this.form.get('password')?.value
    }
    // console.log(user);

    this._loginService.Login(user).subscribe(data => {
      console.log(data);
    }, error =>{
      console.error('Ha ocurrido un error en el logeo' + error);
    })
  }*/

}
