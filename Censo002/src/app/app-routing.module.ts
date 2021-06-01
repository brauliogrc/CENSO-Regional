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
  { path: 'home', component: HomeComponent },
  { path: 'folioanonimo', component: FolioanonimoComponent },
  { path: 'usuarios', component: UsuariosComponent },
  { path: 'localidades', component: LocalidadesComponent },
  { path: 'temas', component: TemasComponent },
  { path: 'areas', component: AreasComponent },
  { path: 'preguntas', component: PreguntasComponent },
  { path: 'tikets', component: TiketsComponent },
  { path: 'folioanonimoindex', component: FolioanonimoindexComponent },
  { path: 'fvacio', component: FvacioComponent },
  { path: 'login', component: LoginComponent },
  { path: 'paneladmin', component: PaneladminComponent },
  { path: 'panelusuario', component: PanelusuarioComponent },
  { path: 'panelusuariobusq', component: PanelusuariobusqComponent },
  { path: '**', pathMatch: 'full', redirectTo: 'home' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
