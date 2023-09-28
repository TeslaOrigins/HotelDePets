import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tutor } from '../models/Tutor';
import { Alimento } from '../models/Alimento';

@Injectable({
  providedIn: 'root'
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
    return this.http.get<Alimento[]>(`${this.base_url}`);
  }
  getAlimentoById(idAlimento: number): Observable<Alimento> {
    return this.http.get<Alimento>(`${this.base_url}${idAlimento}`);
  }
}
