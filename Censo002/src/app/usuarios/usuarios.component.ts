import { Component, OnInit } from '@angular/core';
import { dataUsers } from '../interfaces/interfaces';
import { ShrUsersService } from '../services/hruser/shr-users.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  Users : dataUsers [] = [];

  constructor( private _service:ShrUsersService ) { }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(){
    this._service.getUsers().subscribe(data => {
      this.Users = [... data];
    }, error => {
      console.error( 'Error getting data ' + error );
    });
  }
}
