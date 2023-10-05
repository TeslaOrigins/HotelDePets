import { Component, Inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  ];

  displayedColumns: string[] = ['nome', 'quantidade'];

  constructor(public dialog: MatDialog, private _snackBar: MatSnackBar) {}

  openDialog(medicamento?: Medicamento): void {
    const dialogRef = this.dialog.open(DialogMedicamento, {
      data: { medicamento: medicamento },
    });

    dialogRef.afterClosed().subscribe((result: Medicamento) => {
      if (result) {
        if (result.medicamentoId !== null) {
          const index = this.mockMedicamentos.findIndex(
            (med) => med.medicamentoId === result.medicamentoId
          );
          this.mockMedicamentos = [
            ...this.mockMedicamentos.slice(0, index),
            result,
            ...this.mockMedicamentos.slice(index + 1),
          ];
          this._snackBar.open('Medicamento Atualizado com sucesso', 'X', {
            duration: 3000,
          });
        } else {
          this.mockMedicamentos = [...this.mockMedicamentos, result];
          this._snackBar.open('Medicamento cadastrado com sucesso', 'X', {
            duration: 3000,
          });
        }
      }
    });
  }

  deletarMedicamento(medicamento: Medicamento) {
    if (confirm('Tem certeza que deseja excluir?')) {
      this.mockMedicamentos = this.mockMedicamentos.filter(
        (med) => med.medicamentoId !== medicamento.medicamentoId
      );
    }
  }
  editarMedicamento(medicamento: Medicamento) {
    this.openDialog(medicamento);
  }
}

@Component({
  selector: 'dialog-medicamento',
  templateUrl: '../dialog-medicamento.component.html',
})
export class DialogMedicamento {
  medicamentoForm: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<DialogMedicamento>,
    @Inject(MAT_DIALOG_DATA) public data: { medicamento: Medicamento },
    private builder: FormBuilder
  ) {
    const medicamento = data.medicamento;
    this.medicamentoForm = this.builder.group({
      medicamentoId: new FormControl<number>(medicamento?.medicamentoId),
      nome: new FormControl<string>(medicamento?.nome, Validators.required),
      quantidade: new FormControl<number>(
        medicamento?.quantidade,
        Validators.required
      ),
    });
  }

  onSubmit(): void {
    if (this.medicamentoForm.valid)
      this.dialogRef.close(Object.assign(this.medicamentoForm.value));
  }

  close(): void {
    this.dialogRef.close();
  }
}
