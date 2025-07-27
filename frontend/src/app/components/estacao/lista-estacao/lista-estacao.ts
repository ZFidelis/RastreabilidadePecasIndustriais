import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../../services/apiService/api-service';

@Component({
  selector: 'app-lista-estacao',
  imports: [RouterLink],
  templateUrl: './lista-estacao.html',
  styleUrl: './lista-estacao.css'
})
export class ListaEstacao{
  private _apiService = inject(ApiService);
  listaEstacoes: any[] = [];

  constructor() {
    this._apiService.getEstacoes().subscribe({
      next: (data) => {
        this.listaEstacoes = data;
      },
      error: (err) => {
        console.error('Erro ao buscar estacoes: ', err)
      }
    })
  }

  deletarEstacao(id: number) {
    console.log(id)
    this._apiService.deleteEstacao(id).subscribe({
      next: (data) => {
        console.log(data);
        location.reload();
      },
      error: (err) => {
        console.error('Erro ao deletar estação: ', err)
      }
    })
  }
}

