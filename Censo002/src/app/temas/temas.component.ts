import { Component, OnInit } from '@angular/core';
// import { SthemeService } from '../services/theme/stheme.service';
import { dataTheme } from '../interfaces/interfaces';
import { DataTableService } from '../services/tables/data-table.service';

@Component({
  selector: 'app-temas',
  templateUrl: './temas.component.html',
  styleUrls: ['./temas.component.css']
})
export class TemasComponent implements OnInit {

  theme :dataTheme [] = [];

  constructor( private _service:DataTableService ) { }

  ngOnInit(): void {
    this.getAllTheme();
  }

  getAllTheme(){
    this._service.tableTheme().subscribe(data => {
      this.theme = [... data];
    }, error => {{
      console.error( 'Error getting data ' + error );
    }
  });
  }
}
