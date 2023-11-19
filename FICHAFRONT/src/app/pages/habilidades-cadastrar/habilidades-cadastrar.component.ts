import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { FichaRpg } from 'src/app/models/ficharpg.model';
import { Habilidades } from 'src/app/models/habilidades.model';

@Component({
  selector: 'app-habilidades-cadastrar',
  templateUrl: "./habilidades-cadastrar.component.html",
  styleUrls: ["./habilidades-cadastrar.component.css"
  ]
})
export class HabilidadesCadastrarComponent {
  habilidades = {
    nome: '',
    pontos: '',
    idfichaRpg: ''
  };
  ficharpg: FichaRpg[] = [];
  
  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() : void {
    this.carregarFichasRpg();
  }


  cadastrar_habilidade(): void{

    if (!this.habilidades.nome || !this.habilidades.pontos || !this.habilidades.idfichaRpg) {
      alert('Por favor, preencha todos os campos!');
      return; 
    }
    
    let habilidades: Habilidades = {
      nome: this.habilidades.nome,
      pontos: Number(this.habilidades.pontos),
      idFichaRpg: Number(this.habilidades.idfichaRpg)
    }
    console.log(habilidades);

    this.client.post<Habilidades>("http://localhost:5033/api/Habilidades", habilidades). subscribe({
      next: (habilidades) => {
        alert('Habilidades Cadastrado com sucesso!');
        this.router.navigate(['/pages/habilidades-listar']);
      },
      error: (erro)=>{
        console.log(erro);
      },
    });
  }

  bloquearNaoNumericos(event: KeyboardEvent): void {
    if (['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'Backspace', 'ArrowLeft', 'ArrowRight', 'Tab'].indexOf(event.key) === -1) {
      event.preventDefault();
    }
  }

  carregarFichasRpg(): void {
    this.client.get<FichaRpg[]>("http://localhost:5033/api/FichasRpg").subscribe({
      next: (ficharpg) => {
        this.ficharpg = ficharpg;
        console.log(ficharpg)
      },
      error: (erro) => {
        console.log(erro);
      }
    });
  }
}
