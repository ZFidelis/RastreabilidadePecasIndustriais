import { Component, inject } from '@angular/core';
import { ApiService } from '../../services/apiService/api-service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cadastro-peca',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './cadastro-peca.html',
  styleUrl: './cadastro-peca.css'
})
export class CadastroPeca {
  private _apiService = inject(ApiService);

  novaPeca = {
    partnumber: '',
    descricao: ''
  }

  submitPeca() {
    this._apiService.postPeca(this.novaPeca.partnumber, this.novaPeca.descricao). subscribe({
      next: () => {
        console.log("Peça cadastrada com sucesso!");
        this.novaPeca = { partnumber: '', descricao: '' };
      },
      error: () => console.log("Erro ao Cadastrar peça")
    })
  }
}
