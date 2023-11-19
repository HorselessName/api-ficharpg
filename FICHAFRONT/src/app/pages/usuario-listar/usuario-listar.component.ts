import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario.model';

@Component({
  selector: 'app-usuario-listar',
  templateUrl: "./usuario-listar.component.html",
  styleUrls: ["./usuario-listar.component.css"],
})
export class UsuarioListarComponent {

  usuarios: Usuario[] = [];

  constructor(private client: HttpClient, private router: Router,   private snackBar: MatSnackBar){

  }

  ngOnInit() : void{
    this.client.get<Usuario[]>("http://localhost:5033/api/Usuarios").subscribe({
      next: (usuarios) =>{
        this.usuarios = usuarios;
      },
      error: (erro) => {
        console.log(erro);
      }
    });
  }

  deletarUsuario(id_usuario?: number): void {
    if (confirm('Tem certeza que deseja deletar este usuário?') && id_usuario) {
      this.client.delete(`http://localhost:5033/api/Usuarios/${id_usuario}`).subscribe({
        next: () => {
          this.snackBar.open('Usuario deletado com sucesso!', 'Fechar', { duration: 3000 });
          this.usuarios = this.usuarios.filter(u => u.idUsuario !== id_usuario);
        },
        error: (erro) => {
          console.error(erro);
          alert('Ocorreu um erro ao deletar o usuário.');
        }
      });
    }
  }

  habilitarEdicao(usuario: Usuario): void {
    usuario.editing = true;
}

salvarEdicao(usuario: Usuario): void {
  this.client.put(`http://localhost:5033/api/Usuarios/${usuario.idUsuario}`, usuario)
      .subscribe({
          next: () => {
              this.snackBar.open('Usuario atualizada com sucesso!', 'Fechar', { duration: 3000 });
              usuario.editing = false;
          },
          error: erro => console.error(erro)
      });
}
  
}
