import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario.model';

@Component({
  selector: 'app-usuario-cadastrar',
  templateUrl: "./usuario-cadastrar.component.html",
  styleUrls: ["./usuario-cadastrar.component.css"],
})

export class UsuarioCadastrarComponent {
  usuario = {
    nome: '',
    email: ''
  };

  constructor(private client: HttpClient, private router: Router){}

  cadastrar_usuario(): void{
    if (!this.usuario.nome || !this.usuario.email) {
      alert('Por favor, preencha todos os campos!');
      return; 
    }
    let usuario: Usuario = {
      nome: this.usuario.nome,
      email: this.usuario.email
    }

    this.client.post<Usuario>("http://localhost:5033/api/Usuarios", usuario). subscribe({
      next: (usuario) => {
        alert('Usuário Cadastrado com sucesso!');
        this.router.navigate(['/pages/usuario-listar']);
      },
      error: (erro)=>{
        console.log(erro);
      },
    });
  }
}
