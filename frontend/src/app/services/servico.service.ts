import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Servico } from '../models/Servico';

@Injectable({
  providedIn: 'root',
})
export class ServicoService {
  base_url = environment.api_url + 'servico/';

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Servico> {
    return this.http.post<Servico>(`${this.base_url}`, body);
  }
  editar(body: any): Observable<Servico> {
    return this.http.put<Servico>(`${this.base_url}${body.servidoId}`, body);
  }

  deletar(servidoId: number): Observable<string> {
    return this.http.delete<string>(`${this.base_url}${servidoId}`);
  }
  getAllServicos(): Observable<Servico[]> {
    //return this.http.get<Servico[]>(`${this.base_url}`);
    return of([
      { servicoId: 0, nome: 'tosa', descricao: 'tosa do pet', preco: 80 },
      { servicoId: 1, nome: 'banho', descricao: 'banho do pet', preco: 50 },
      {
        servicoId: 2,
        nome: 'cortar unha',
        descricao: 'corta unha do pet',
        preco: 20,
      },
      {
        servicoId: 3,
        nome: 'passear',
        descricao: 'leva o pet para passear',
        preco: 5,
      },
    ]);
  }
}
