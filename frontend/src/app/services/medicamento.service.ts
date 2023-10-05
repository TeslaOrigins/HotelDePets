import { HttpClient, HttpBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Medicamento } from '../models/Medicamento';

@Injectable({
  providedIn: 'root',
})
export class MedicamentoService {
  base_url = environment.api_url + 'medicamento/';

  constructor(private http: HttpClient, handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  cadastrar(body: any): Observable<Medicamento> {
    return this.http.post<Medicamento>(`${this.base_url}`, body);
  }
  editar(body: any): Observable<Medicamento> {
    return this.http.put<Medicamento>(
      `${this.base_url}${body.medicamentoId}`,
      body
    );
  }

  deletar(medicamentoId: number): Observable<string> {
    return this.http.delete<string>(`${this.base_url}${medicamentoId}`);
  }
  getAllMedicamentos(): Observable<Medicamento[]> {
    //return this.http.get<Medicamento[]>(`${this.base_url}`);
    return of([
      {
        medicamentoId: 0,
        nome: 'dipirona 30mg',
        quantidade: 3,
      },
      {
        medicamentoId: 1,
        nome: 'paracetamol 15mg',
        quantidade: 8,
      },
      {
        medicamentoId: 2,
        nome: 'paracetamol 30mg',
        quantidade: 8,
      },
      {
        medicamentoId: 3,
        nome: 'paracetamol 45mg',
        quantidade: 8,
      },
    ]);
  }
}
