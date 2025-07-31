import { Component, inject } from '@angular/core';
import { ApiService } from '../../services/apiService/api-service';
import { FormsModule } from '@angular/forms';

interface Estacao {
  id: number,
  nome: string,
  inventario: string,
  ordem: number
}

@Component({
  selector: 'app-home',
  imports: [FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})

export class Home {
  private _apiService = inject(ApiService);
  listaEstacoes: Estacao[] = [];
  estacaoSelecionada: Estacao[] | null = null;

  partnumber = '';
  estacaoDestino = '';
  responsavel = '';
  observacao = '';

  estacaoDestinoEnvio = 0

  constructor() {
    this._apiService.getEstacoes().subscribe({
      next: (data) => {
        this.listaEstacoes = data;
        this.listaEstacoes = [...this.listaEstacoes].sort((a,b) => a.ordem - b.ordem);
      },
      error: (err) => {
        console.error('Erro ao buscar pecas: ', err)
      }
    })
  }

  selecionarEstacao(estacao: Estacao) {
    this.estacaoSelecionada = [estacao];
  }


  submitForm() {
    console.log("Dados de envio:", this.partnumber, this.estacaoSelecionada?.[0].id, this.responsavel, this.observacao);
    this._apiService.postMovimentacao(this.partnumber, this.estacaoSelecionada?.[0].id, this.responsavel, this.observacao).subscribe({
            next: () => {
              console.log("Movimentação Concluída!")
              this.partnumber = '', this.estacaoDestino = '', this.responsavel = '', this.observacao = '';
            },
            error: (err) => {
              console.log(err)
            }
          });
  }
}
