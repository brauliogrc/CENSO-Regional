import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { LocalidadesComponent } from './localidades/localidades.component';
import { TemasComponent } from './temas/temas.component';
import { AreasComponent } from './areas/areas.component';
import { PreguntasComponent } from './preguntas/preguntas.component';
import { TiketsComponent } from './tikets/tikets.component';
import { FolioanonimoComponent } from './folioanonimo/folioanonimo.component';
import { FvacioComponent } from './fvacio/fvacio.component';
import { LoginComponent } from './login/login.component';
import { PaneladminComponent } from './paneladmin/paneladmin.component';
import { PanelusuarioComponent } from './panelusuario/panelusuario.component';
import { PanelusuariobusqComponent } from './panelusuariobusq/panelusuariobusq.component';
import { FolioanonimoindexComponent } from './folioanonimoindex/folioanonimoindex.component';
import { BienvenidoComponent } from './bienvenido/bienvenido.component';
import { ReportesComponent } from './reportes/reportes.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },                           // uncionalidad de registro de folio y busquesa de folio anonimo (Flata estilo de la tabla)
  { path: 'folioanonimo', component: FolioanonimoComponent },           // ELIMINAR
  { path: 'usuarios', component: UsuariosComponent },                   // Funcionalidad completa
  { path: 'localidades', component: LocalidadesComponent },             // Funcionalidad completa
  { path: 'temas', component: TemasComponent },                         // Funcionalidad completa
  { path: 'areas', component: AreasComponent },                         // Falte seccion para agragar una nueva area
  { path: 'preguntas', component: PreguntasComponent },                 // Funcionalidad completa
  { path: 'tikets', component: TiketsComponent },                       // Funcionalidad completa
  { path: 'folioanonimoindex', component: FolioanonimoindexComponent }, // ELIMINAR (Su contenido fue movido al componente home)
  { path: 'fvacio', component: FvacioComponent },                       // Eliminar
  { path: 'login', component: LoginComponent },                         // Funcionalidad parcialmente completa
  {
    path: 'paneladmin', component: PaneladminComponent,
    children: [

      { path: 'bienvenido', component: BienvenidoComponent },
      { path: 'usuarios', component: UsuariosComponent },
      { path: 'localidades', component: LocalidadesComponent },
      { path: 'temas', component: TemasComponent },
      { path: 'areas', component: AreasComponent },
      { path: 'preguntas', component: PreguntasComponent },
      { path: 'tikets', component: TiketsComponent },
      { path: 'reportes', component: ReportesComponent },
      { path: '**', pathMatch: 'full', redirectTo: 'bienvenido' },

    ]
  },               //
  { path: 'panelusuario', component: PanelusuarioComponent },           // Funcionalidad completa
  { path: 'panelusuariobusq', component: PanelusuariobusqComponent },   //
  { path: '**', pathMatch: 'full', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
