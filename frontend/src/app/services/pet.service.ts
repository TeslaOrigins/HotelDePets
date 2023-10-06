import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tutor } from '../models/Tutor';
import { Pet } from '../models/Pet';

@Injectable({
  providedIn: 'root',
})
export class PetService {
  base_url = environment.api_url + 'pet/';

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Pet> {
    return this.http.post<Pet>(`${this.base_url}`, body);
  }

  alterar(body: any): Observable<Pet> {
    return this.http.put<Pet>(`${this.base_url}${body.petId}`, body);
  }

  apagar(petId: number): Observable<string> {
    return this.http.delete<string>(`${this.base_url}${petId}`);
  }
  getAllPets(): Observable<Pet[]> {
    return of([
      {
        petId: 1,
        nome: 'juba',
        idadeMes: 3,
        raca: 'nobre',
        sexo: 'Masculino',
        fotoUrl:
          'https://img.freepik.com/free-photo/puppy-that-is-walking-snow_1340-37228.jpg',
        peso: 30,
        especie: 'nobreza',
      },
      {
        petId: 2,
        nome: 'jujuba',
        idadeMes: 6,
        raca: 'nobre',
        sexo: 'Feminino',
        fotoUrl:
          'https://img.freepik.com/free-photo/puppy-that-is-walking-snow_1340-37228.jpg',
        peso: 60,
        especie: 'nobreza',
      },
      {
        petId: 3,
        nome: 'raimundo',
        idadeMes: 12,
        raca: 'ruim',
        sexo: 'Masculino',
        fotoUrl:
          'https://images.unsplash.com/photo-1598875706250-21faaf804361?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8NHx8fGVufDB8fHx8fA%3D%3D&w=1000&q=80',
        peso: 10,
        especie: 'vira-lata',
      },
    ]);
    return this.http.get<Pet[]>(`${this.base_url}`);
  }
  getPetById(idPet: number): Observable<Pet> {
    return this.http.get<Pet>(`${this.base_url}${idPet}`);
  }
}
