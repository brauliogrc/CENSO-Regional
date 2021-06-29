import { Component, OnInit } from '@angular/core';
import { DataTableService } from '../services/tables/data-table.service';
import { dataTickets } from '../interfaces/interfaces';
import { SearchesService } from '../services/searches/searches.service';

@Component({
  selector: 'app-tikets',
  templateUrl: './tikets.component.html',
  styleUrls: ['./tikets.component.css']
})
export class TiketsComponent implements OnInit {

  Tikets : dataTickets[] = [];

  tiket : any;

  constructor(
    private _ticketService    : DataTableService,
    private _searchService    : SearchesService
  ) { }

  ngOnInit(): void {

    // Obtencion de los tickets registrados
    this._ticketService.tableTickets().subscribe (data => {
      this.Tikets = [... data];
      console.log(this.Tikets);
    }, error => {
      console.error(error.error.message);
    })
  }

  search(tiketId : any){
    this._searchService.searchFolio(tiketId).subscribe( data => {
      this.tiket = data;
      this.Tikets = [];
      console.log(data);
    }, error => {
      console.log(error.error.message)
    })  
  }
}
