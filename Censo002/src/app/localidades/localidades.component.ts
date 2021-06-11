import { Component, OnInit } from '@angular/core';
// import { SlocationsService } from '../services/locations/slocations.service';
import { dataLocations } from '../interfaces/interfaces';
import { DataTableService } from '../services/tables/data-table.service';

@Component({
  selector: 'app-localidades',
  templateUrl: './localidades.component.html',
  styleUrls: ['./localidades.component.css']
})
export class LocalidadesComponent implements OnInit {

  locations : dataLocations[] = [];

  constructor( private _service:DataTableService ) { }

  ngOnInit(): void {
    this.getAllLocations();
  }

  getAllLocations(){
    this._service.tableLocations().subscribe( data => {
      console.log(data);
      
      this.locations = [... data];
    }, error =>{
      console.error( 'Error getting data ' + error );
    });
  }
}
