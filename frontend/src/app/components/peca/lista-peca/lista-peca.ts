import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../../services/apiService/api-service';


@Component({
  selector: 'app-lista-peca',
  imports: [RouterLink],
  templateUrl: './lista-peca.html',
  styleUrl: './lista-peca.css'
})
export class ListaPeca {
  private _apiService = inject(ApiService);

  listaPecas: any[] = [];

  constructor() {
    this._apiService.getPecas().subscribe({
      next: (data) => {
        this.listaPecas = data;
      },
      error: (err) => {
        console.error('Erro ao buscar pecas: ', err)
      }
    })
  }

  deletarPeca(id: number) {
    this._apiService.deletePeca(id).subscribe({
      next: (data) => {
        console.log(data);
        location.reload();
      },
      error: (err) => {
        console.error('Erro ao deletar peça: ', err)
      }
    })
  }
}
