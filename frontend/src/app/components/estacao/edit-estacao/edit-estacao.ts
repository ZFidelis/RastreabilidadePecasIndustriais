import { Component, inject } from '@angular/core';
import { ApiService } from '../../../services/apiService/api-service';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-estacao',
  imports: [FormsModule],
  templateUrl: './edit-estacao.html',
  styleUrl: './edit-estacao.css'
})
export class EditEstacao {
  private _apiService = inject(ApiService)

  estacao = {
    nome: '',
    descricao: '',
    inventario: '',
    ordem: 0,
    ativo: true
  }

  id: number = 0
  constructor(private route: ActivatedRoute, private router: Router) {
    this.id = +(this.route.snapshot.paramMap.get('id') ?? '0');
    console.log(this.id)
    this._apiService.getEstacaoById(this.id).subscribe({
      next: (data) => {
        this.estacao = data;
      },
      error: (err) => {
        console.error('Erro ao buscar estação: ', err)
      }
    })
  }


  submitEditEstacao() {
    this._apiService.putEstacao(this.id, this.estacao.nome, this.estacao.descricao, this.estacao.inventario, this.estacao.ordem, this.estacao.ativo).subscribe({
      next: (data) => {
        console.log(data)
        this.router.navigate(['/lista-estacao'])
      },
      error: (err) => {
        console.error('Erro ao editar estação: ', err)
      }
    })
  }
}
