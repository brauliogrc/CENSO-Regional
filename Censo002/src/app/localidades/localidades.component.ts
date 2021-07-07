import { Component, OnInit } from '@angular/core';
import { SlocationsService } from '../services/locations/slocations.service';
import { dataLocations, dataNewLocation } from '../interfaces/interfaces';
import { DataTableService } from '../services/tables/data-table.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SearchesService } from '../services/searches/searches.service';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-localidades',
  templateUrl: './localidades.component.html',
  styleUrls: ['./localidades.component.css'],
})
export class LocalidadesComponent implements OnInit {
  // Array que contiene la informacion de las localidades disponibles para ser mostradas en la tabla
  locations: dataLocations[] = [];

  // Toma los valores de la localidad buscada por medio del id
  location: any;

  newLocation = this._fb.group({
    lName: ['', [Validators.required, Validators.maxLength(50)]],
    lStatus: ['', [Validators.required]],
  });

  constructor(
    private _service: DataTableService,
    private _locationService: SlocationsService,
    private _fb: FormBuilder,
    private _searches: SearchesService,
    private _authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.validRole();
  }

  validRole(): void {
    if (Number(sessionStorage.getItem('role')) != 1) {
      console.error('Sección no accesible');
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getAllLocations();
  }

  // Obtenemos las localidades desde el service
  getAllLocations() {
    this._service.tableLocations().subscribe(
      (data) => {
        console.log(data);

        this.locations = [...data];
      },
      (error) => {
        console.error('Error getting data ' + error);
      }
    );
  }

  addNewLocation() {
    const dataNewLocation: dataNewLocation = {
      lName: this.newLocation.get('lName')?.value,
      lStatus: this.newLocation.get('lStatus')?.value,
    };
    console.log(dataNewLocation);

    this._locationService.addNewLocation(dataNewLocation).subscribe(
      (data) => {
        this.newLocation.reset();
        console.log('Localidad registrada con el Id: ' + data.lId);
        alert(
          `Se ha registrado la localidad ${data.lName} con el id ${data.lId}.`
        );
        this.getAllLocations();
      },
      (error) => {
        console.error(error);
      }
    );
  }

  deleteLocation(id: number) {
    this._locationService.deleteLocation(id).subscribe(
      (data) => {
        console.log('Localidad eliminada');
        alert(`Localidad "${data.lName}" eliminada`);
        this.getAllLocations();
      },
      (error) => {
        console.error(error);
      }
    );
  }

  // Busca una localidad con base al id dado
  search(idLocation: any) {
    this._searches.getSpecificLocation(idLocation).subscribe(
      (data) => {
        this.location = data;
        this.locations = [];
        console.log(this.location);
      },
      (error) => {
        alert(error.error.message);
      }
    );
  }
}
