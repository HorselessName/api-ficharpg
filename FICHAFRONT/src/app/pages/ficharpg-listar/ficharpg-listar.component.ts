import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FichaRpg } from 'src/app/models/ficharpg.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Usuario } from 'src/app/models/usuario.model';

@Component({
  selector: 'app-ficharpg-listar',
  templateUrl: "./ficharpg-listar.component.html",
  styleUrls: ["./ficharpg-listar.component.css"
  ]
})
export class FicharpgListarComponent {
  fichasrpg: FichaRpg[] = [];
  usuarios: Usuario[] = []; 

  constructor(private client: HttpClient, private router: Router, private snackBar: MatSnackBar){}


  ngOnInit() : void{
    this.client.get<FichaRpg[]>("http://localhost:5033/api/FichasRpg").subscribe({
      next: (fichasrpg) =>{
        this.fichasrpg = fichasrpg;
        console.log(fichasrpg, 'sdadsa')
      },
      error: (erro) => {
        console.log(erro);
      }
    });
    this.carregarUsuarios();
  }

    deletarFicha(idFichaRpg?: number): void {
      const confirmacao = confirm('Tem certeza que deseja deletar esta ficha?');
      if (confirmacao) {
        this.client.delete(`http://localhost:5033/api/FichasRpg/${idFichaRpg}`)
          .subscribe({
            next: () => {
              this.snackBar.open('Ficha deletada com sucesso!', 'Fechar', { duration: 3000 });
              this.router.navigate(['/pages/ficharpg-listar']);
              location.reload();
            },
            error: (erro) => {
              console.error(erro);
            }
          });
      }
    }
    carregarUsuarios(): void {
      this.client.get<Usuario[]>("http://localhost:5033/api/Usuarios").subscribe({
        next: (usuarios) => {
          this.usuarios = usuarios;
        },
        error: (erro) => {
          console.log(erro);
        }
      });
    }
    getUserName(idUsuario: number): string {
      const usuario = this.usuarios.find(u => u.idUsuario === idUsuario);
      return usuario ? usuario.nome : 'Desconhecido';
    }

    habilitarEdicao(ficha: FichaRpg): void {
        ficha.editing = true;
    }

    salvarEdicao(ficha: FichaRpg): void {
      this.client.put(`http://localhost:5033/api/FichasRpg/${ficha.idFichaRpg}`, ficha)
          .subscribe({
              next: () => {
                  this.snackBar.open('Ficha atualizada com sucesso!', 'Fechar', { duration: 3000 });
                  ficha.editing = false;
              },
              error: erro => console.error(erro)
          });
  }
  bloquearNaoNumericos(event: KeyboardEvent): void {
    if (['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'Backspace', 'ArrowLeft', 'ArrowRight', 'Tab'].indexOf(event.key) === -1) {
      event.preventDefault();
    }
  }


}
