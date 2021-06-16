import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { dataUsers } from '../interfaces/interfaces';
// import { ShrUsersService } from '../services/hruser/shr-users.service';
import { DataTableService } from '../services/tables/data-table.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  Users : dataUsers [] = [];
  newUser = this._fb.group({

  });

  constructor(  private _service:DataTableService,
                private _fb:FormBuilder) { }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(){
    this._service.tableUsers().subscribe(data => {
      console.log(data);
      
      this.Users = [... data];
    }, error => {
      console.error( 'Error getting data ' + error );
    });
  }

  addNewUser(){}
}
