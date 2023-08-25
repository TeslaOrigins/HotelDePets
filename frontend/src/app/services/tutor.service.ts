import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tutor } from '../models/Tutor';

@Injectable({
  providedIn: 'root'
})
export class TutorService {
  base_url = environment.api_url + 'tutor/';

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Tutor> {
    return this.http.post<Tutor>(`${this.base_url}cadastrar`, body);
  }

  getAllTutores(): Observable<Tutor[]> {
    return this.http.get<Tutor[]>(`${this.base_url}all`);
  }
}
