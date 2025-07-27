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

  existe: any = false

  submitEstacao() {
    this._apiService.verificacaoInventario(this.novaEstacao.inventario).subscribe({
      next: (existeR) => {
        this.existe = existeR;
        if (!existeR) {
          this._apiService.postEstacao(this.novaEstacao.nome, this.novaEstacao.descricao, this.novaEstacao.inventario, this.novaEstacao.ordem). subscribe({
            next: () => {
              console.log("Estação cadastrada com sucesso!");
              this.novaEstacao = { nome: '', descricao: '', inventario: '', ordem: 0 };
              this.existe = false;
            },
            error: (err) => {
              console.log("Erro ao Cadastrar estação", err)
            }
        });
        }
        else {
          console.log("Invetário já existe");
        }
      },
      error: (err) => {
        console.error('Erro ao buscar Invetário: ', err);
        this.novaEstacao = { nome: '', descricao: '', inventario: '', ordem: 0 };
        this.existe = false;
      }
    })
  }

}
