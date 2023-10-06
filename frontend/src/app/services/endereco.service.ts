import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Endereco } from '../models/Endereco'; // Certifique-se de importar o modelo correto

@Injectable({
  providedIn: 'root'
})
export class EnderecoService {
  private base_url = environment.api_url + 'api/enderecos'; // Altere o caminho da API conforme necessário

  constructor(private http: HttpClient) {}

  cadastrarEndereco(endereco: Endereco): Observable<Endereco> {
    return this.http.post<Endereco>(this.base_url, endereco);
  }

  editarEndereco(endereco: Endereco): Observable<Endereco> {
    const url = `${this.base_url}/${endereco.id}`; // Substitua "id" pelo nome correto da propriedade de identificação
    return this.http.put<Endereco>(url, endereco);
  }

  deletarEndereco(enderecoId: number): Observable<string> {
    const url = `${this.base_url}/${enderecoId}`;
    return this.http.delete<string>(url);
  }

  getEnderecosByTutorId(tutorId: number): Observable<Endereco[]> {
    const url = `${this.base_url}?tutor_id=${tutorId}`;
    return this.http.get<Endereco[]>(url);
  }
}