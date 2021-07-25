import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';
import { ListService } from '../services/newServices/List/list.service';
import { UserService } from '../services/newServices/user/user.service';
import { SearchService } from '../services/newServices/Search/search.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import {
  addUser,
  Location,
  Rol,
  userList,
  searchData,
  locationList,
} from '../interfaces/newInterfaces';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css'],
})
export class UsuariosComponent implements OnInit {
  // Array que contendra los datos de los usuarios de la base de datos para mostrar en la tabla
  Users: userList[] = [];

  user: any;

  // Array que contendra los datos de las locations de la base de datos para mostrar en la lista desplegable
  Locations: Location[] = [];

  // Array que contendra los datos de los Roles de la base de datos para mostrar en la lista desplegable
  Roles: Rol[] = [];

  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Cada campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
   */
  newUser = this._fb.group({
    uName: ['', [Validators.required, Validators.maxLength(50)]],
    uEmail: ['', [Validators.required, Validators.maxLength(80)]],
    RolId: ['', [Validators.required]],
    uStatus: ['', [Validators.required]],
    EmployeeNumber: ['', [Validators.required]],
  });

  constructor(
    private _listService: ListService,
    private _userSerice: UserService,
    private _searchService: SearchService,
    private _authService: AuthService,
    private _fieldsService: FieldsService,
    private _fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.validRole();
  }

  validRole(): void {
    if (
      Number(sessionStorage.getItem('role')) != 1 &&
      Number(sessionStorage.getItem('role')) != 2
    ) {
      console.error('Sección no accesible');
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getUserList();
    this.getlocations();
    this.getRoles();
  }

  getlocations(): void {
    // Obtenemos las localidades disponibles
    this._fieldsService.getLocations().subscribe(
      (data) => {
        this.Locations = [...data];
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Obtencion de los roles disponibles
  getRoles(): void {
    this._fieldsService.getRoles().subscribe(
      (data) => {
        this.Roles = [...data];
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Obtenemos los datos de los usuarios para mostrarlos en la tabla
  getUserList(): void {
    this._listService
      .getUserList(Number(sessionStorage.getItem('location')))
      .subscribe(
        (data) => {
          this.Users = [...data];
        },
        (error) => {
          console.error(error.error.message);
        }
      );
  }

  addNewUser(): void {
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const dataNewUser: addUser = {
      uName: this.newUser.get('uName')?.value,
      uEmail: this.newUser.get('uEmail')?.value,
      RolId: this.newUser.get('RolId')?.value,
      uStatus: this.newUser.get('uStatus')?.value,
      EmployeeNumber: this.newUser.get('EmployeeNumber')?.value,
      uCreationUser: Number(sessionStorage.getItem('userId')),
    };
    console.log(dataNewUser);

    // Registro del nuevo usuario en la tabla HRU
    this._userSerice.addNewUser(dataNewUser).subscribe(
      (data) => {
        console.log(data.message);
        this.getUserList();
        this.newUser.reset();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Borrado logico del usuario
  deleteUser(userId: number): void {
    this._userSerice.deleteUser(userId).subscribe(
      (data) => {
        console.log(data.message);
        this.user = null;
        this.getUserList();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Busqueda de un usuario en especifico
  search(userId: string): void {
    if (userId) {
      // Definicion de los datos de busqueda
      let userSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: Number(userId),
      };

      this._searchService.searchUser(userSearch).subscribe(
        (data) => {
          this.user = data;
          this.Locations = [];
        },
        (error) => {
          console.error(error.error.message);
        }
      );
    }
  }
}
