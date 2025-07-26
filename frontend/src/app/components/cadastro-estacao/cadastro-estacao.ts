import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/apiService/api-service';

@Component({
  selector: 'app-cadastro-estacao',
  imports: [FormsModule],
  templateUrl: './cadastro-estacao.html',
  styleUrl: './cadastro-estacao.css'
})
export class CadastroEstacao {
  private _apiService = inject(ApiService);

  novaEstacao = {
    nome: '',
    descricao: '',
    inventario: '',
    ordem: 0
  }

  submitEstacao() {
    this._apiService.postEstacao(this.novaEstacao.nome, this.novaEstacao.descricao, this.novaEstacao.inventario, this.novaEstacao.ordem). subscribe({
      next: () => {
        console.log("Estação cadastrada com sucesso!");
        this.novaEstacao = { nome: '', descricao: '', inventario: '', ordem: 0 };
      },
      error: () => console.log("Erro ao Cadastrar estação")
    })
  }
}
