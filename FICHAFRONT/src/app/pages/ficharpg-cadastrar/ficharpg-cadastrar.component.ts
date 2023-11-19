import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FichaRpg } from 'src/app/models/ficharpg.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Usuario } from 'src/app/models/usuario.model';

@Component({
  selector: 'app-ficharpg-cadastrar',
  templateUrl:"./ficharpg-cadastrar.component.html" ,
  styleUrls: ["./ficharpg-cadastrar.component.css"],
})
export class FicharpgCadastrarComponent {
  ficharpg = {
    Nivel: '',
    Antecedencia: '',
    NomeDoJogador: '',
    Raca: '',
    Alinhamento: '',
    PontosDeExperiencia: '',
    IdUsuario: ''
  };
  usuarios: Usuario[] = [];

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() : void {
    this.carregarUsuarios();
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

  bloquearNaoNumericos(event: KeyboardEvent): void {
  if (['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'Backspace', 'ArrowLeft', 'ArrowRight', 'Tab'].indexOf(event.key) === -1) {
    event.preventDefault();
  }
}

  cadastrar_ficharpg(): void{
  if (!this.ficharpg.Nivel || !this.ficharpg.Antecedencia || !this.ficharpg.NomeDoJogador || 
    !this.ficharpg.Raca || !this.ficharpg.Alinhamento || !this.ficharpg.PontosDeExperiencia || 
    !this.ficharpg.IdUsuario) {
      alert('Por favor, preencha todos os campos.');
      return; 
    }
    let ficharpg: FichaRpg = {
      nivel: Number(this.ficharpg.Nivel),
      antecedencia: this.ficharpg.Antecedencia,
      nomeDoJogador: this.ficharpg.NomeDoJogador,
      raca: this.ficharpg.Raca,
      alinhamento: this.ficharpg.Alinhamento,
      pontosDeExperiencia: Number(this.ficharpg.PontosDeExperiencia),
      idUsuario: Number(this.ficharpg.IdUsuario)}

      console.log(ficharpg);

    this.client.post<FichaRpg>("http://localhost:5033/api/FichasRpg", ficharpg).subscribe({
      next:(ficharpg) =>{
        alert('Ficha Cadastrada com sucesso!');
        this.router.navigate(['/pages/ficharpg-listar']);
      },
      error: (erro)=>{
        console.log(erro);
      },
    });
  }
}
