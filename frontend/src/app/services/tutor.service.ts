import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tutor } from '../models/Tutor';

@Injectable({
  providedIn: 'root',
})
export class TutorService {
  base_url = environment.api_url + 'tutor/';
  //base_url = 'http://localhost:3000/tutores'

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Tutor> {
    return this.http.post<Tutor>(`${this.base_url}`, body);
  }
  alterar(body: any): Observable<Tutor> {
    return this.http.put<Tutor>(`${this.base_url}${body.tutorId}`, body);
  }

  apagar(tutorId: number): Observable<string> {
    return this.http.delete<string>(`${this.base_url}${tutorId}`);
  }
  getAllTutores(): Observable<Tutor[]> {
    return of([
      {
        tutorId: 0,
        nome: 'tutor 1',
        nomeNormalizado: 'tut',
        cpf: '0523232412',
        telefone: '79999999999',
        email: 'supertutor@gmail.com',
        enderecos: [
          {
            enderecoId: 0,
            logradouro: 'rua um dois tres',
            numero: '77',
            cidade: 'Aracaju',
            estado: 'Sergipe',
          },
        ],
      },
      {
        tutorId: 1,
        nome: 'tutor 2',
        nomeNormalizado: 'tut2',
        cpf: '0523234412',
        telefone: '79999999989',
        email: 'supertutor2@gmail.com',
        enderecos: [
          {
            enderecoId: 0,
            logradouro: 'rua um dois tres',
            numero: '77',
            cidade: 'Aracaju',
            estado: 'Sergipe',
          },
        ],
      },
    ]);
    //return this.http.get<Tutor[]>(`${this.base_url}`);
  }
  getTutorById(idTutor: number): Observable<Tutor> {
    return this.http.get<Tutor>(`${this.base_url}${idTutor}`);
  }
}
