import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';
import { ListService } from '../services/newServices/List/list.service';
import { UserService } from '../services/newServices/user/user.service';
import { SearchService } from '../services/newServices/Search/search.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { userInformation } from '../../assets/ts/interfaces/newInterfaces';
import {
  addUser,
  Location,
  Rol,
  userList,
  searchData,
} from '../../assets/ts/interfaces/newInterfaces';

// import { CreateScriptsService } from '../services/newServices/CreateScripts/create-scripts.service';
import { Popup } from '../../assets/ts/popup';

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

  private userInformation: userInformation = {
    email: '',
    employeeNumber: 0,
    name: '',
    location: '',
  };

  flag: boolean = false;

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
    // private _createScript: CreateScriptsService,
    private _listService: ListService,
    private _userSerice: UserService,
    private _searchService: SearchService,
    private _authService: AuthService,
    private _fieldsService: FieldsService,
    private _fb: FormBuilder,
    private router: Router
  ) {
    // this._createScript.CargaArchivos( [ "popoupEdicion" ] );
  }

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
    this.flag = false;
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

  // Validacion de tecla precionada
  eventHandler(event: any, employeeNumber: string) {
    // console.log(event, event.keyCode, event.keyIdentifier);
    // console.log(event.keyCode, event.keyIdentifier);
    let keyCode: number = event.keyCode;
    console.log(keyCode);
    if (keyCode === 32) {
      this._userSerice.getUserInformation(Number(employeeNumber)).subscribe(
        (data) => {
          this.userInformation.email = data.email;
          this.userInformation.employeeNumber = data.employeeNumber;
          this.userInformation.name = data.name;
          this.userInformation.location = data.location;
          console.log(this.userInformation);
        },
        (error) => {
          console.error(error.error.message);
        }
      );
    }
  }

  // Muestra el nombre del usuario
  getUserName = (): string => {
    if (this.userInformation.name.length > 4) {
      this.flag = true;
    }
    return this.userInformation.name;
  };

  // Muestra el correo del usuario
  getEmail = (): string | null => {
    // console.log(this.newUser.get('uEmail')?.value);
    // this.userInformation.email?.split(" ").join("");
    if (this.userInformation.email?.length != 0) {
      // console.log(this.userInformation.email);
      return this.userInformation.email;
    }
    // console.log(this.newUser.get('uEmail')?.value);

    return this.newUser.get('uEmail')?.value;
  };

  // Muestra la localidad del ususario
  getLocation = (): string => {
    return this.userInformation.location;
  };

  addNewUser(): void {
    if (this.flag) {
      /**
       * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
       */

      // TODO
      const dataNewUser: addUser = {
        uName: this.getUserName(),
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
    } else {
      console.log('Datos invalidos');
    }
  }

  // Borrado logico del usuario
  deleteUser(userId: number): void {
    // console.log(userId);

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
  search(employeeNumber: string): void {
    // console.log(employeeNumber);

    if (employeeNumber) {
      // Definicion de los datos de busqueda
      let userSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: Number(employeeNumber),
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

  private popup = new Popup();
  // Llamado de modals
  mostrar() {
    this.popup.mostrar();
  }

  mostrarTema() {
    this.popup.mostrarTema();
  }

  cerrar() {
    this.popup.cerrar();
  }

  cerrarTema() {
    this.popup.cerrarTema();
  }
}
