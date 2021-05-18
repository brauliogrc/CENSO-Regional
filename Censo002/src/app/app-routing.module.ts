import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from "./home/home.component"; 
import { UsuariosComponent } from "./usuarios/usuarios.component" ;
import { LocalidadesComponent } from "./localidades/localidades.component";
import { TemasComponent } from "./temas/temas.component";
import { AreasComponent } from "./areas/areas.component";
import { PreguntasComponent } from "./preguntas/preguntas.component";
import { TiketsComponent } from "./tikets/tikets.component";


const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'usuarios', component: UsuariosComponent },
  { path: 'localidades', component: LocalidadesComponent },
  { path: 'temas', component: TemasComponent },
  { path: 'areas', component: AreasComponent },
  { path: 'preguntas', component: PreguntasComponent },
  { path: 'tikets', component: TiketsComponent },
  { path: '**', pathMatch: 'full', redirectTo: 'home' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
