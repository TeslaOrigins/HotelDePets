import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Medicamento } from 'src/app/models/Medicamento';

@Component({
  selector: 'app-listar-medicamentos',
  templateUrl: './listar-medicamentos.component.html',
  styleUrls: ['./listar-medicamentos.component.scss'],
})
export class ListarMedicamentosComponent {
  mockMedicamentos: Medicamento[] = [
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
  ];

  displayedColumns: string[] = ['nome', 'quantidade'];
}
