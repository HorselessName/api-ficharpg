import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsuarioCadastrarComponent } from './pages/usuario-cadastrar/usuario-cadastrar.component';
import { UsuarioListarComponent } from './pages/usuario-listar/usuario-listar.component';
import { FicharpgCadastrarComponent } from './pages/ficharpg-cadastrar/ficharpg-cadastrar.component';
import { FicharpgListarComponent } from './pages/ficharpg-listar/ficharpg-listar.component';
import { HabilidadesCadastrarComponent } from './pages/habilidades-cadastrar/habilidades-cadastrar.component';
import { HabilidadesListarComponent } from './pages/habilidades-listar/habilidades-listar.component';

const routes: Routes = [
  {
    path : "",
    component : UsuarioCadastrarComponent
  },
  {
    path : "pages/usuario-listar",
    component : UsuarioListarComponent
  },
  {
    path : "pages/usuario-cadastrar",
    component : UsuarioCadastrarComponent
  },
  {
    path : "pages/ficharpg-cadastrar",
    component : FicharpgCadastrarComponent
  },
  {
    path : "pages/ficharpg-listar",
    component : FicharpgListarComponent
  },
  {
    path : "pages/habilidades-cadastrar",
    component : HabilidadesCadastrarComponent
  },
  {
    path : "pages/habilidades-listar",
    component : HabilidadesListarComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
