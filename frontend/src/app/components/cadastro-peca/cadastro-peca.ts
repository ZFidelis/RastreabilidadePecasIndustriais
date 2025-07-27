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
  existe: any = false

  submitPeca() {
    this._apiService.verificacaoPartnumber(this.novaPeca.partnumber).subscribe({
      next: (existeR) => {
        this.existe = existeR;
        if (!existeR) {
          this._apiService.postPeca(this.novaPeca.partnumber, this.novaPeca.descricao).subscribe({
            next: () => {
              console.log("Peça cadastrada com sucesso!")
              this.novaPeca = { partnumber: '', descricao: '' };
              this.existe = false;
            },
            error: (err) => {
              console.log("Erro ao cadastrar peça", err)
            }
          });
        }
        else {
          console.log("Partnumber já existe");
        }
      },
      error: (err) => {
        console.error('Erro ao verificar Partnumber: ', err);
        this.novaPeca = { partnumber: '', descricao: '' };
        this.existe = false;
      }
    });
  }


}
