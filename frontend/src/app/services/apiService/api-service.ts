import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5201/api'

  constructor(private http: HttpClient) { }

  getPecas(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Peca`)
  }

  getEstacoes(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Estacao`)
  }

  getMovimentacoes(partnumber: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/HistoricoMovimentacao/${partnumber}`)
  }

  postPeca(partnumber: string, descricao: string) {
    return this.http.post(`${this.apiUrl}/Peca`, { partnumber, descricao })
  }

  postEstacao(nome: string, descricao: string, inventario: string, ordem: number) {
    return this.http.post(`${this.apiUrl}/Estacao`, { nome, descricao, inventario, ordem })
  }

  verificacaoPartnumber(partnumber: string) {
    return this.http.get(`${this.apiUrl}/Peca/partnumber-existe/${partnumber}`)
  }

  verificacaoInventario(inventario: string) {
    return this.http.get(`${this.apiUrl}/Estacao/inventario-existe/${inventario}`)
  }
}
