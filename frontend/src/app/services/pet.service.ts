import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tutor } from '../models/Tutor';
import { Pet } from '../models/Pet';

@Injectable({
  providedIn: 'root'
})
export class PetService {
  base_url = environment.api_url + 'pet/';

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Pet> {
    return this.http.post<Pet>(`${this.base_url}`, body);
  }
  editar(body: any): Observable<Pet> {
    return this.http.put<Pet>(`${this.base_url}${body.petId}`, body);
  }

  deletar(petId: number): Observable<string>{
    return this.http.delete<string>(`${this.base_url}${petId}`);
  }
  getAllPets(): Observable<Pet[]> {
    return this.http.get<Pet[]>(`${this.base_url}`);
  }
  getPetById(idPet: number): Observable<Pet>{
    return this.http.get<Pet>(`${this.base_url}${idPet}`);
  }
}
