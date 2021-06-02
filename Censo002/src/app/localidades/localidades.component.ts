import { Component, OnInit } from '@angular/core';
import { SlocationsService } from '../services/locations/slocations.service';
import { dataLocations } from '../interfaces/interfaces';

@Component({
  selector: 'app-localidades',
  templateUrl: './localidades.component.html',
  styleUrls: ['./localidades.component.css']
})
export class LocalidadesComponent implements OnInit {

  locations : dataLocations[] = [];

  constructor( private _service:SlocationsService ) { }

  ngOnInit(): void {
    this.getAllLocations();
  }

  getAllLocations(){
    this._service.getLocations().subscribe( data => {
      this.locations = [... data];
    }, error =>{
      console.error( 'Error getting data ' + error );
    });
  }
}
