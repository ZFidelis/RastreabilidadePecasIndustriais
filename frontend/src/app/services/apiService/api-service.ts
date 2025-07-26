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
}
