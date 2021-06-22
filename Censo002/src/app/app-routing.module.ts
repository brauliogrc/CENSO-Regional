import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from "./home/home.component"; 
import { UsuariosComponent } from "./usuarios/usuarios.component" ;
import { LocalidadesComponent } from "./localidades/localidades.component";
import { TemasComponent } from "./temas/temas.component";
import { AreasComponent } from "./areas/areas.component";
import { PreguntasComponent } from "./preguntas/preguntas.component";
import { TiketsComponent } from "./tikets/tikets.component";
import { FolioanonimoComponent } from "./folioanonimo/folioanonimo.component";
import { FvacioComponent } from "./fvacio/fvacio.component";
import { LoginComponent } from "./login/login.component";
import { PaneladminComponent } from "./paneladmin/paneladmin.component";
import { PanelusuarioComponent } from "./panelusuario/panelusuario.component";
import { PanelusuariobusqComponent } from "./panelusuariobusq/panelusuariobusq.component";
import { FolioanonimoindexComponent } from "./folioanonimoindex/folioanonimoindex.component";


const routes: Routes = [
  { path: 'home', component: HomeComponent }, // Pagina principal
  { path: 'folioanonimo', component: FolioanonimoComponent }, // **TO DO** 
  { path: 'usuarios', component: UsuariosComponent }, // Vista con una tabla que muestra a los usuarios y su informacion
  { path: 'localidades', component: LocalidadesComponent }, // Vista con una tabla que muestra a las localidades y su informacion
  { path: 'temas', component: TemasComponent }, // Vista con una tabla que muestra a los temas y su informacion
  { path: 'areas', component: AreasComponent }, // Vista con una tabla que muestra a las areas y su informacion **TO DO**
  { path: 'preguntas', component: PreguntasComponent }, // Vista con una tabla que muestra a las preguntas y su informacion
  { path: 'tikets', component: TiketsComponent }, // **TO DO** 
  { path: 'folioanonimoindex', component: FolioanonimoindexComponent }, // Formulario de registro de peticion anonima
  { path: 'fvacio', component: FvacioComponent },
  { path: 'login', component: LoginComponent }, // Formulario de logueo
  { path: 'paneladmin', component: PaneladminComponent },
  { path: 'panelusuario', component: PanelusuarioComponent }, // Formulario de registro de peticion
  { path: 'panelusuariobusq', component: PanelusuariobusqComponent },
  { path: '**', pathMatch: 'full', redirectTo: 'home' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
