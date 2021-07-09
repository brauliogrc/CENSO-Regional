import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { LocalidadesComponent } from './localidades/localidades.component';
import { TemasComponent } from './temas/temas.component';
import { AreasComponent } from './areas/areas.component';
import { PreguntasComponent } from './preguntas/preguntas.component';
import { TiketsComponent } from './tikets/tikets.component';
import { HomeComponent } from './home/home.component';
import { FolioanonimoComponent } from './folioanonimo/folioanonimo.component';
import { FvacioComponent } from './fvacio/fvacio.component';
import { FolioanonimoindexComponent } from './folioanonimoindex/folioanonimoindex.component';
import { LoginComponent } from './login/login.component';
import { PanelusuarioComponent } from './panelusuario/panelusuario.component';
import { PanelusuariobusqComponent } from './panelusuariobusq/panelusuariobusq.component';
import { PaneladminComponent } from './paneladmin/paneladmin.component';

import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { BienvenidoComponent } from './bienvenido/bienvenido.component';
import { RespuestaFolioComponent } from './respuesta-folio/respuesta-folio.component';


@NgModule({
  declarations: [
    AppComponent,
    UsuariosComponent,
    LocalidadesComponent,
    TemasComponent,
    AreasComponent,
    PreguntasComponent,
    TiketsComponent,
    HomeComponent,
    FolioanonimoComponent,
    FvacioComponent,
    FolioanonimoindexComponent,
    LoginComponent,
    PanelusuarioComponent,
    PanelusuariobusqComponent,
    PaneladminComponent,
    BienvenidoComponent,
    RespuestaFolioComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NoopAnimationsModule,
    MatSelectModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
