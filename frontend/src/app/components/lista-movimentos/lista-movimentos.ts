import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/apiService/api-service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-lista-movimentos',
  imports: [FormsModule, DatePipe],
  templateUrl: './lista-movimentos.html',
  styleUrl: './lista-movimentos.css'
})
export class ListaMovimentos {
  private _apiService = inject(ApiService);

  listaMovimentacoes: any[] = [];

  partnumber = ''

  buscaMovimentos() {
    this._apiService.getMovimentacoes(this.partnumber). subscribe({
      next: (data) => {
        this.partnumber = '';
        this.listaMovimentacoes = data;
        console.log(data)
      },
      error: (err) => {
        console.log("Erro ao buscar movimentacoes", err);
        this.listaMovimentacoes = [];
      }
    })
  }

}
