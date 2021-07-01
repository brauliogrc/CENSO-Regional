import { Component, OnInit } from '@angular/core';
import { dataTheme, dataNewTheme, availableLocations } from '../interfaces/interfaces';
import { DataTableService } from '../services/tables/data-table.service';
import { FormBuilder, Validators } from '@angular/forms';
import { SthemeService } from '../services/theme/stheme.service';
import { SearchesService } from '../services/searches/searches.service';

@Component({
  selector: 'app-temas',
  templateUrl: './temas.component.html',
  styleUrls: ['./temas.component.css']
})
export class TemasComponent implements OnInit {
  
  // Array que conendrás los datos de los temas para mostrarlos en la tabla
  theme :dataTheme [] = [];

  th : any;

  // Array con las localidades disponibles para la lista desplegable
  Locations : availableLocations [] = [];

  newTheme = this._fb.group({
    tName: ['', [Validators.required, Validators.maxLength(50)]],
    LocationId: ['', [Validators.required]],
    tStatus: ['', [Validators.required]]
  });

  constructor(  private _service:DataTableService,
                private _fb:FormBuilder,
                private _themeService:SthemeService,
                private _searches:SearchesService) { }

  ngOnInit(): void {
    this.getAllTheme();

    // Obtenemos las Locations disponibles
    this._service.tableLocations().subscribe(data => {
      this.Locations = [... data];
    }, error =>{
      console.error(error);
    })
  }

  getAllTheme(){
    this._service.tableTheme().subscribe(data => {
      this.theme = [... data];
    }, error => {{
      console.error( 'Error getting data ' + error );
    }
  });
  }

  addNewTheme(){
    const dataNewTheme : dataNewTheme ={
      tName: this.newTheme.get('tName')?.value,
      tStatus: this.newTheme.get('tStatus')?.value,
      LocationId: this.newTheme.get('LocationId')?.value
    }
    console.log(dataNewTheme);
    
    this._themeService.addNewTheme(dataNewTheme).subscribe( data => {
      console.log(data);
      this.newTheme.reset();
      alert(`Nuevo tema ${data.tName} registrado.`);
      this.getAllTheme();
    }, error => {
      console.error(error);
    })
  }

  deleteTheme(id : number){
    this._themeService.deleteTheme(id).subscribe( data => {
      console.log('Tema eliminado');
      alert(`Tema "${data.tName}" eliminado`);
      this.getAllTheme();
    })
  }

  search(idTheme : any){
    this._searches.getSpecificTheme(idTheme).subscribe( data => {
      this.th = data;
      this.theme = [];
      console.log(this.th);
    }, error => {
      alert(error.error.message);
    })
  }
}
