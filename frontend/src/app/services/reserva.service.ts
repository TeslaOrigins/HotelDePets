import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Reserva } from '../models/Reserva';

@Injectable({
  providedIn: 'root',
})
export class ReservaService {
  base_url = environment.api_url + 'reserva/';

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Reserva> {
    return this.http.post<Reserva>(`${this.base_url}`, body);
  }
  editar(body: any): Observable<Reserva> {
    return this.http.put<Reserva>(`${this.base_url}${body.reservaId}`, body);
  }

  deletar(reservaId: number): Observable<string> {
    return this.http.delete<string>(`${this.base_url}${reservaId}`);
  }
  getAllreservas(): Observable<Reserva[]> {
    //return this.http.get<Reserva[]>(`${this.base_url}`);
    return of([
      {
        reservaId: 0,
        dataCheckin: new Date('2023-10-04'),
        dataCheckout: new Date('2023-10-08'),
        servicos: [
          { servicoId: 0, nome: 'tosa', descricao: 'tosa do pet', preco: 80 },
          { servicoId: 1, nome: 'banho', descricao: 'banho do pet', preco: 50 },
        ],
      },
      {
        reservaId: 1,
        dataCheckin: new Date('2023-10-05'),
        dataCheckout: new Date('2023-10-09'),
        servicos: [
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
        ],
      },
      {
        reservaId: 2,
        dataCheckin: new Date('2023-10-06'),
        dataCheckout: new Date('2023-11-01'),
        servicos: [
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
        ],
      },
    ]);
  }
}
