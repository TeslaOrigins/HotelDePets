import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tutor } from '../models/Tutor';

@Injectable({
  providedIn: 'root'
})
export class TutorService {
  base_url = environment.api_url + 'api/tutors';

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Tutor> {
    return this.http.post<Tutor>(`${this.base_url}`, body);
  }
  editar(tutorId: number,body: any): Observable<Tutor> {
    return this.http.put<Tutor>(`${this.base_url}/${tutorId}`, body);
  }

  deletar(tutorId: number): Observable<string>{
    return this.http.delete<string>(`${this.base_url}/${tutorId}`);
  }
  getAllTutores(): Observable<Tutor[]> {
    return this.http.get<Tutor[]>(`${this.base_url}`);
  }
  getTutorById(idTutor: number): Observable<Tutor>{
    return this.http.get<Tutor>(`${this.base_url}/${idTutor}`);
  }
}
