import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';
import { ListService } from '../services/newServices/List/list.service';
import { UserService } from '../services/newServices/user/user.service';
import { SearchService } from '../services/newServices/Search/search.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import {
  userInformation,
  existingUser,
  userChanges,
  addUser,
  Location,
  Rol,
  userList,
  searchData,
  Theme,
  userTheme,
} from '../../assets/ts/interfaces/newInterfaces';

import { Popup } from '../../assets/ts/popup';
// import { ThemeList } from '../../assets/ts/theme-list';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse } from '@angular/common/http';

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
    private router: Router,
    private _fb: FormBuilder,
    private _userSerice: UserService,
    private _listService: ListService,
    private _authService: AuthService,
    private _showError: ShowErrorService,
    private _searchService: SearchService,
    private _fieldsService: FieldsService
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
      this._showError.NotAccessible();
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
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Obtencion de los roles disponibles
  getRoles(): void {
    let retryRequest: number = 0;
    this._fieldsService.getRoles().subscribe(
      (data) => {
        this.Roles = [...data];
      },
      (error: HttpErrorResponse) => {
        retryRequest = retryRequest + 1;
        if (retryRequest < 2) {
          // this.getRoles();
          window.location.reload();
        } else {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
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
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
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
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
    }
  }

  // Muestra el nombre del usuario
  getUserName = (): string => {
    if (this.userInformation.name.length != 0) {
      this.flag = true;
    }
    return this.userInformation.name;
  };

  // Muestra el correo del usuario
  getEmail = (): string | null => {
    return this.userInformation.email;
  };

  // Muestra la localidad del ususario
  getLocation = (): string => {
    return this.userInformation.location;
  };

  addNewUser(): void {
    // if (this.flag) {
      /**
       * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
       */

      // TODO
      const dataNewUser: addUser = {
        uName: this.getUserName(),
        uEmail: this.getEmail(),
        RolId: this.newUser.get('RolId')?.value,
        uStatus: this.newUser.get('uStatus')?.value,
        EmployeeNumber: this.newUser.get('EmployeeNumber')?.value,
        uCreationUser: Number(sessionStorage.getItem('userId')),
      };

      const name: string = this.newUser.get('uName').value;
      const email: string = this.newUser.get('uEmail').value;

      if (name.length != 0 && name != this.getUserName()) {
        dataNewUser.uName = name;
      }
      if (email.length != 0 && email != this.getEmail()) {
        dataNewUser.uEmail = email;
      }

      console.log(dataNewUser);

      // Registro del nuevo usuario en la tabla HRU
      this._userSerice.addNewUser(dataNewUser).subscribe(
        (data) => {
          console.log(data.message);
          this._showError.success(data.message);
          this.getUserList();
          this.newUser.reset();
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
    // } else {
      // console.log('Datos invalidos');
    // }
  }

  formVlidate() {
    
  }

  // Borrado logico del usuario
  deleteUser(userId: number): void {
    console.log(userId);

    this._userSerice.deleteUser(userId).subscribe(
      (data) => {
        console.log(data.message);
        this._showError.success(data.message);
        this.user = null;
        this.getUserList();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
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
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
    }
  }

  // Llamado de modals y actualizacion del usuario
  private popup = new Popup();
  // themeList = new ThemeList();
  editFlag: boolean = false;
  userData: existingUser; // Información antual del usuario
  relatedTopics: Theme[] = []; // Listado de los temas del usuario
  themeList: Theme[] = []; // Listado de los temas diponibles

  mostrar(employeeNumber: number) {
    // this.userData = null;
    this.relatedTopics = [];
    this.getlocations();
    this.getUserData(employeeNumber);
    this.getRelatedTopics(employeeNumber);
    this.popup.mostrar();
  }

  cerrar() {
    this.updateUser.reset();
    this.editFlag = false;
    this.popup.cerrar();
  }

  mostrarTema(employeeNumber: number) {
    // this.userData = null;
    this.relatedTopics = [];
    this.getUserData(employeeNumber);
    this.getRelatedTopics(employeeNumber);
    this.getThemeList(Number(sessionStorage.getItem('location')));
    this.popup.mostrarTema();
  }

  cerrarTema() {
    this.popup.cerrarTema();
  }

  // Obtencion de la información del usuario
  getUserData(employeeNumber: number) {
    this._searchService.getExistingUser(employeeNumber).subscribe(
      (data) => {
        this.userData = data[0];
        this.editFlag = true;
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Obtención de los temas relacionados al usuario a editar
  getRelatedTopics(employeeNumber: number) {
    this._searchService.getRelatedTopics(employeeNumber).subscribe(
      (data) => {
        // this.setTheme([...data]);
        this.relatedTopics = [...data];
        console.log(this.relatedTopics);
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Seteo de los temas
  // setTheme(data): void {
  //   for (let theme of data) {
  //     this.themeList.addNewTheme(theme);
  //   }
  //   console.log(this.themeList.getThemeList);
  // }

  updateUser = this._fb.group({
    newName: ['', [Validators.maxLength(50)]],
    newEmail: ['', [Validators.maxLength(80)]],
    newRol: [''],
    newStatus: [''],
    newLocation: [''],
  });

  // Método de modificació de campos del usuario
  saveChanges(): void {
    const saveChanges: userChanges = {
      modificationUser: Number(sessionStorage.getItem('employeeNumber')),
      employeeNumber: this.userData.uEmployeeNumber,
      uName: this.updateUser.get('newName').value,
      uEmail: this.updateUser.get('newEmail').value,
      uStatus: this.updateUser.get('newStatus').value,
      roleId: this.updateUser.get('newRol').value,
      LocationId: this.updateUser.get('newLocation').value,
    };

    // Llamada al método de actualización
    this._userSerice.userUpdate(saveChanges).subscribe(
      (data) => {
        console.log('Usuario actualizado');
        this._showError.success(data.message);
        this.getUserList();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Optención del index del tema a eliminar
  getIdx(themeId: number) {
    this.relatedTopics.find((item, idx) => {
      if (item.tId === themeId) {
        console.log(idx);
        this.deleteRelatedTopics(item.tId);
      }
    });
  }

  // Borrado de una relación entre un usuario y un tema
  deleteRelatedTopics(themeId: number): void {
    // Llamada al método de eliminacion de la relacion entre el usuario y el tema
    this._userSerice
      .deleteRelatedTopic(this.userData.uEmployeeNumber, themeId)
      .subscribe(
        (data) => {
          console.log(data.message);
          this._showError.success(data.message);
          this.getRelatedTopics(this.userData.uEmployeeNumber);
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
  }

  // Añadiendo un tema aun usuario
  addRelatedTopic(themeId: number): void {
    console.log(themeId);

    const newRelation: userTheme = {
      employeeNumber: this.userData.uEmployeeNumber,
      themeId: themeId,
    };
    this._userSerice.addRelatedTopic(newRelation).subscribe(
      (data) => {
        console.log(data.message);
        this._showError.success(data.message);
        this.getRelatedTopics(this.userData.uEmployeeNumber);
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Obtención de los temas disponibles
  getThemeList(locationId: number): void {
    this.themeList = [];
    this._fieldsService.getThme(locationId).subscribe(
      (data) => {
        console.log(data);

        this.themeList = [...data];
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }
}
