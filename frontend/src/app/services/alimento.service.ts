import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tutor } from '../models/Tutor';
import { Alimento } from '../models/Alimento';

@Injectable({
  providedIn: 'root',
})
export class AlimentoService {
  base_url = environment.api_url + 'alimento/';

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Alimento> {
    return this.http.post<Alimento>(`${this.base_url}`, body);
  }
  alterar(body: any): Observable<Alimento> {
    return this.http.put<Alimento>(`${this.base_url}${body.tutorId}`, body);
  }

  apagar(alimentoId: number): Observable<string> {
    return this.http.delete<string>(`${this.base_url}${alimentoId}`);
  }
  getAllAlimentos(): Observable<Alimento[]> {
    return of([
      {
        alimentoId: 0,
        nome: 'carne',
        quantidadeEstoque: 32,
        precoReabastecimento: 50,
        dataEntrada: new Date('04/10/2023'),
      },
      {
        alimentoId: 1,
        nome: 'ração',
        quantidadeEstoque: 40,
        precoReabastecimento: 20,
        dataEntrada: new Date('02/10/2023'),
      },
    ]);
    return this.http.get<Alimento[]>(`${this.base_url}`);
  }
  getAlimentoById(idAlimento: number): Observable<Alimento> {
    return this.http.get<Alimento>(`${this.base_url}${idAlimento}`);
  }
}
