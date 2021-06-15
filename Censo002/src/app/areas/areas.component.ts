import { Component, OnInit } from '@angular/core';
import { DataTableService } from '../services/tables/data-table.service';
import { dataArea } from '../interfaces/interfaces';

@Component({
  selector: 'app-areas',
  templateUrl: './areas.component.html',
  styleUrls: ['./areas.component.css']
})
export class AreasComponent implements OnInit {

  areas : dataArea[] = []; // Asignar a una interface

  constructor(private _service:DataTableService) { }

  ngOnInit(): void {
    this.getAllAreas();
  }

  getAllAreas(){
    this._service.tableAreas().subscribe(data => {
      console.log(data);
      this.areas = [... data];
      
    }, error => {
      console.error( 'Error getting data ' +  error);
    })
  }
}
