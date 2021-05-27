import { Component, OnInit } from '@angular/core';

// Importación del servicio
import { SlocationsService } from '../services/slocations.service';

import { dataLocations } from '../interfaces/interfaces';

@Component({
  selector: 'app-localidades',
  templateUrl: './localidades.component.html',
  styleUrls: ['./localidades.component.css']
})
export class LocalidadesComponent implements OnInit {

  locations: dataLocations[] = []; // Crear un interface para este arreglo
  // Inyección de dependencias del servicio
  constructor( private _service:SlocationsService ) { }

  ngOnInit(): void {
    this.getAllLocations();
  }

  getAllLocations(){
    this._service.getLocations().subscribe(data => {
      console.log(data);
      this.locations = [... data];
    }, error => {
      console.error( 'Error getting data ' + error);
    });
  }
}
