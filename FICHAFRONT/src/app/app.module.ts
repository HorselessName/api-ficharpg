import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { MatExpansionModule } from '@angular/material/expansion';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UsuarioCadastrarComponent } from './pages/usuario-cadastrar/usuario-cadastrar.component';
import { UsuarioListarComponent } from './pages/usuario-listar/usuario-listar.component';
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatListModule } from "@angular/material/list";
import { MatTableModule } from "@angular/material/table";
import { MatCardModule } from "@angular/material/card";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { FicharpgCadastrarComponent } from './pages/ficharpg-cadastrar/ficharpg-cadastrar.component';
import { FicharpgListarComponent } from './pages/ficharpg-listar/ficharpg-listar.component';
import { HabilidadesCadastrarComponent } from './pages/habilidades-cadastrar/habilidades-cadastrar.component';
import { HabilidadesListarComponent } from './pages/habilidades-listar/habilidades-listar.component';

@NgModule({
  declarations: [
    AppComponent,
    UsuarioCadastrarComponent,
    UsuarioListarComponent,
    FicharpgCadastrarComponent,
    FicharpgListarComponent,
    HabilidadesCadastrarComponent,
    HabilidadesListarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatTableModule,
    MatCardModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatExpansionModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
