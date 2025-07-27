import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../services/apiService/api-service';
import { Observable } from 'rxjs';

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
        console.error('Erro ao buscar pecas: ', err)
      }
    })
  }
}

