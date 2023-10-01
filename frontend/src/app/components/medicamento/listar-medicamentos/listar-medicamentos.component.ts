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
  ];

  displayedColumns: string[] = ['nome', 'quantidade'];

  constructor(public dialog: MatDialog, private _snackBar: MatSnackBar) {}

  openDialog(pet?: Medicamento): void {
    const dialogRef = this.dialog.open(DialogMedicamento);

    dialogRef.afterClosed().subscribe((result) => {
      console.log(result);
      if (result) {
        this.mockMedicamentos = [...this.mockMedicamentos, result];
        this._snackBar.open('Medicamento cadastrado com sucesso', 'X', {
          duration: 3000,
        });
      }
    });
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
    @Inject(MAT_DIALOG_DATA) public data: Medicamento,
    private builder: FormBuilder
  ) {
    this.medicamentoForm = this.builder.group({
      nome: new FormControl<string>('', Validators.required),
      quantidade: new FormControl<number>(0, Validators.required),
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
