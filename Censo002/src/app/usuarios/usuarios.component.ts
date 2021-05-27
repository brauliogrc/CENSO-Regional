import { Component, OnInit } from '@angular/core';
import { ShrUsersService } from '../services/shr-users.service';
import { dataUsers, dataLocations } from '../interfaces/interfaces';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  Users: dataUsers[] = [];

  constructor( private _service:ShrUsersService ) { }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(){
    this._service.getUsers().subscribe(data => {
      console.log(data);
      this.Users = [... data];
    }, error => {
      console.error( 'Error getting data ' + error);
    });
  }
}
