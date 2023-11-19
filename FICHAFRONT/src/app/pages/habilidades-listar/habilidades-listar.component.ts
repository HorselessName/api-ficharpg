import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { FichaRpg } from 'src/app/models/ficharpg.model';
import { Habilidades } from 'src/app/models/habilidades.model';

@Component({
  selector: 'app-habilidades-listar',
  templateUrl: "./habilidades-listar.component.html",
  styleUrls: ["./habilidades-listar.component.css"]
})
export class HabilidadesListarComponent {

  habilidades: Habilidades[] = [];
  ficharpg: FichaRpg[] = [];

  constructor(private client: HttpClient, private router: Router,   private snackBar: MatSnackBar){}

  ngOnInit() : void{
    this.client.get<Habilidades[]>("http://localhost:5033/api/Habilidades").subscribe({
      next: (habilidades) =>{
        this.habilidades = habilidades;
      },
      error: (erro) => {
        console.log(erro);
      }
    });
    this.carregarFicha();
  }

  deletarHabilidade(id_Habilidade?: number): void {
    if (confirm('Tem certeza que deseja deletar esta habilidade?') && id_Habilidade) {
      this.client.delete(`http://localhost:5033/api/Usuarios/${id_Habilidade}`).subscribe({
        next: () => {
          this.snackBar.open('Usuario deletado com sucesso!', 'Fechar', { duration: 3000 });
          this.habilidades = this.habilidades.filter(u => u.idHabilidade !== id_Habilidade);
        },
        error: (erro) => {
          console.error(erro);
          alert('Ocorreu um erro ao deletar o usuário.');
        }
      });
    }
  }

  carregarFicha(): void {
    this.client.get<FichaRpg[]>("http://localhost:5033/api/Usuarios").subscribe({
      next: (ficharpg) => {
        this.ficharpg = ficharpg;
      },
      error: (erro) => {
        console.log(erro);
      }
    });
  }

  getfichaName(id_FichaRpg: number): string {
    const ficharpg = this.ficharpg.find(f => f.idFichaRpg === id_FichaRpg);
    return ficharpg ? ficharpg.nomeDoJogador : 'Desconhecido';
  }

  habilitarEdicao(habilidade: Habilidades): void {
    habilidade.editing = true;
  }

  salvarEdicao(habilidade: Habilidades): void {
    this.client.put(`http://localhost:5033/api/Habilidades/${habilidade.idHabilidade}`, habilidade)
        .subscribe({
            next: () => {
                this.snackBar.open('Habilidade atualizada com sucesso!', 'Fechar', { duration: 3000 });
                habilidade.editing = false;
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
