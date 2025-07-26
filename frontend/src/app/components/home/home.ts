import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { GetBackend } from '../../services/get-backend';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  private getbackService = inject(GetBackend)
  meuTitulo = 'Teste!';
  quem = "Isaac";
  mustShowTitle = false;

  minhaLista = ["1","2","3"]

  submit() {
    this.getbackService.pegaEvento(this.meuTitulo)
  }

  submit2() {
    this.mustShowTitle = !this.mustShowTitle
  }

  submit3() {
    this.entregaParaOutros.emit(this.dadosExternos);
  }

  @Input("algumacoisaExterna") dadosExternos!: string;

  @Output() entregaParaOutros = new EventEmitter<string>();
}
