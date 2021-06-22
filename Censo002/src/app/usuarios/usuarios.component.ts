import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { dataUsers, dataNewUser, availableLocations, availableRoles } from '../interfaces/interfaces';
import { ShrUsersService } from '../services/hruser/shr-users.service';
import { DataTableService } from '../services/tables/data-table.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  // Array que contendra los datos de los usuarios de la base de datos para mostrar en la tabla
  Users : dataUsers [] = [];

  user : any;

  // Array que contendra los datos de las locations de la base de datos para mostrar en la lista desplegable
  Locations : availableLocations [] = [];

  // Array que contendra los datos de los Roles de la base de datos para mostrar en la lista desplegable
  Roles : availableRoles [] = [];

   /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Cada campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
  */
  newUser = this._fb.group({
    uName: ['', [Validators.required, Validators.maxLength(50)]],
    uEmail: ['', [Validators.required, Validators.maxLength(80)]],
    RolId: ['', [Validators.required]],
    uStatus: ['', [Validators.required]],
    LocationId: ['', [Validators.required]],
    EmployeeNumber: ['', [Validators.required]]
  });

  constructor(  private _service:DataTableService,
                private _fb:FormBuilder,
                private _userSerice:ShrUsersService) { }

  ngOnInit(): void {
    this.getAllUsers();

    // Obtenemos las localidades disponibles
    this._service.tableLocations().subscribe(data => {
      console.log(data);
      this.Locations = [... data];
      
    }, error => {
      console.error(error);
    })

    // Obtenemos los Roles disponibles
    this._userSerice.getRoles().subscribe(data => {
      console.log(data);
      this.Roles = [... data];
    }, error => {
      console.error(error);
    })
  }

  // Obtenemos los datos de los usuarios para mostrarlos en la tabla
  getAllUsers(){
    this._service.tableUsers().subscribe(data => {
      console.log(data);
      
      this.Users = [... data];
    }, error => {
      console.error( 'Error getting data ' + error );
    });
  }

  addNewUser(){
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const dataNewUser : dataNewUser = { // Asignar una interface
      uName: this.newUser.get('uName')?.value,
      uEmail: this.newUser.get('uEmail')?.value,
      RolId: this.newUser.get('RolId')?.value,
      uStatus: this.newUser.get('uStatus')?.value,
      LocationId: this.newUser.get('LocationId')?.value,
      EmployeeNumber: this.newUser.get('EmployeeNumber')?.value
    }
    console.log(dataNewUser);
    
    // Llamamos al metodo del servicio encargado de enviar la infromacion a la api apra que registre al usuario
    this._userSerice.addNewUser(dataNewUser).subscribe(data => {
      this.newUser.reset();
      console.log('Usuario registrados con el Id: ' + data.uId);
      alert(`Se ha registrado a "${data.uName}" con el id ${data.uId}.`);
      this.getAllUsers();
    }, error => {
      console.error(error);
    })
  }

  deleteUser(id : number){
    this._userSerice.deleteUser(id).subscribe( data => {
      console.log('Usuario eliminado');
      alert(`Usuario "${data.uName}" eliminado`);
      this.getAllUsers();
    }, error => {
      console.error(error);      
    })
  }

  search(idUser : any){
    this._userSerice.getSpecificUser(idUser).subscribe( data => {
      this.user = data;
      this.Users = [];
      console.log(this.user);
    }, error => {
      alert(error.error.message);
    })
  }
}
