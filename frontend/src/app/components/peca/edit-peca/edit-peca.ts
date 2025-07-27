import { Component, inject } from '@angular/core';
import { ApiService } from '../../../services/apiService/api-service';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-peca',
  imports: [FormsModule],
  templateUrl: './edit-peca.html',
  styleUrl: './edit-peca.css'
})
export class EditPeca {
  private _apiService = inject(ApiService)

  peca = {
    partnumber: '',
    descricao: '',
    ativo: true
  }

  id: number = 0
  constructor(private route: ActivatedRoute, private router: Router) {
    this.id = +(this.route.snapshot.paramMap.get('id') ?? '0');
    this._apiService.getPecaById(this.id).subscribe({
      next: (data) => {
        this.peca = data;
      },
      error: (err) => {
        console.error('Erro ao buscar peça: ', err)
      }
    })
  }

  submitEditPeca() {
    this._apiService.putPeca(this.id, this.peca.partnumber, this.peca.descricao, this.peca.ativo).subscribe({
      next: (data) => {
        console.log(data)
        this.router.navigate(['/lista-peca'])
      },
      error: (err) => {
        console.error('Erro ao editar peça: ', err)
      }
    })
  }

}
