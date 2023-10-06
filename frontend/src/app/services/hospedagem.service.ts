import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Hospedagem } from '../models/Hospedagem'; // Certifique-se de importar o modelo correto

@Injectable({
  providedIn: 'root'
})
export class HospedagemService {
  base_url = environment.api_url + 'hospedagem/'; // Certifique-se de ajustar a URL da API
  //base_url = 'http://localhost:3000/hospedagens'

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Hospedagem> {
    return this.http.post<Hospedagem>(`${this.base_url}`, body);
  }

  editar(body: any): Observable<Hospedagem> {
    return this.http.put<Hospedagem>(`${this.base_url}${body.hospedagemId}`, body);
  }

  deletar(hospedagemId: number): Observable<string> {
    return this.http.delete<string>(`${this.base_url}${hospedagemId}`);
  }

  getAllHospedagens(): Observable<Hospedagem[]> {
    return this.http.get<Hospedagem[]>(`${this.base_url}`);
  }

  getHospedagemById(idHospedagem: number): Observable<Hospedagem> {
    return this.http.get<Hospedagem>(`${this.base_url}${idHospedagem}`);
  }
}