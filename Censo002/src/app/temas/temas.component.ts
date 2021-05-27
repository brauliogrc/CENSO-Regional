import { Component, OnInit } from '@angular/core';
import { SthemeService } from '../services/stheme.service';
import { dataTheme } from '../interfaces/interfaces';

@Component({
  selector: 'app-temas',
  templateUrl: './temas.component.html',
  styleUrls: ['./temas.component.css']
})
export class TemasComponent implements OnInit {

  themes : dataTheme[] = [];

  constructor(private _service:SthemeService) { }

  ngOnInit(): void {
    this.getAllTheme();
  }

  getAllTheme(){
    // Subscripción al métdo del sericio que realizará la petición a la API y nos devolverá los datos de la base de datos
    this._service.getTheme().subscribe(data => {
      console.log(data);
      this.themes = [... data ];
    }, error =>{
      console.error( 'Error getting data ' + error);
    });
  }
}
