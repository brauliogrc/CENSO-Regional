import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { LocalidadesComponent } from './localidades/localidades.component';
import { TemasComponent } from './temas/temas.component';
import { AreasComponent } from './areas/areas.component';
import { PreguntasComponent } from './preguntas/preguntas.component';
import { TiketsComponent } from './tikets/tikets.component';
import { HomeComponent } from './home/home.component';

// Importación del HttpClientModule para la conexión con la api y peticiones HTTP
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    UsuariosComponent,
    LocalidadesComponent,
    TemasComponent,
    AreasComponent,
    PreguntasComponent,
    TiketsComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
