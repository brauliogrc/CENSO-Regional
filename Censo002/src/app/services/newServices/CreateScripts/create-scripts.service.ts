import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CreateScriptsService {

  constructor() { }

  CargaArchivos ( scripts: string[] ) {
    for( let archivo of scripts) {
      let script = document.createElement("script");
      script.src = "../../../assets/js/" + archivo + '.js';
      let body = document.getElementsByTagName("body")[0];
      body.appendChild(script); 
    }
  }
}
