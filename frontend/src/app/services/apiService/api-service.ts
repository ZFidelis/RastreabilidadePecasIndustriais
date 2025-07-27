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

  getPecaById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Peca/${id}`)
  }

  getEstacoes(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Estacao`)
  }

  getEstacaoById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Estacao/${id}`)
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

  putPeca(id: number,partnumber: string, descricao: string, ativo: boolean) {
    return this.http.put(`${this.apiUrl}/Peca/${id}`, { partnumber, descricao, ativo})
  }

  putEstacao(id: number,nome: string, descricao: string, inventario: string, ordem: number, ativo: boolean) {
    return this.http.put(`${this.apiUrl}/Estacao/${id}`, { nome, descricao, inventario, ordem, ativo})
  }

  deletePeca(id: number) {
    return this.http.delete(`${this.apiUrl}/Peca/${id}`)
  }

  deleteEstacao(id: number) {
    return this.http.delete(`${this.apiUrl}/Estacao/${id}`)
  }

  verificacaoPartnumber(partnumber: string) {
    return this.http.get(`${this.apiUrl}/Peca/partnumber-existe/${partnumber}`)
  }

  verificacaoInventario(inventario: string) {
    return this.http.get(`${this.apiUrl}/Estacao/inventario-existe/${inventario}`)
  }

  postMovimentacao(partnumber: string, estacaoDestinoId: any, responsavel: string, observacao: string) {
    return this.http.post(`${this.apiUrl}/HistoricoMovimentacao/movimentar`, { partnumber, estacaoDestinoId, responsavel, observacao })
  }


}
