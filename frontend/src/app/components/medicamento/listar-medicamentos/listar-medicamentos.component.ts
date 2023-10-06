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
import { take } from 'rxjs';

import { Medicamento } from 'src/app/models/Medicamento';
import { MedicamentoService } from 'src/app/services/medicamento.service';

@Component({
  selector: 'app-listar-medicamentos',
  templateUrl: './listar-medicamentos.component.html',
  styleUrls: ['./listar-medicamentos.component.scss'],
})
export class ListarMedicamentosComponent {
  medicamentos: Medicamento[] = [];

  displayedColumns: string[] = ['nome', 'quantidade'];

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private medicamentoService: MedicamentoService
  ) {
    this.medicamentoService
      .getAllMedicamentos()
      .pipe(take(1))
      .subscribe((med) => (this.medicamentos = med));
  }

  openDialog(medicamento?: Medicamento): void {
    const dialogRef = this.dialog.open(DialogMedicamento, {
      data: { medicamento: medicamento },
    });

    dialogRef.afterClosed().subscribe((result: Medicamento) => {
      if (result) {
        if (result.medicamentoId !== null) {
          const index = this.medicamentos.findIndex(
            (med) => med.medicamentoId === result.medicamentoId
          );
          this.medicamentos = [
            ...this.medicamentos.slice(0, index),
            result,
            ...this.medicamentos.slice(index + 1),
          ];
          this._snackBar.open('Medicamento Atualizado com sucesso', 'X', {
            duration: 3000,
          });
          this.medicamentoService.editar(result).pipe(take(1)).subscribe();
        } else {
          this.medicamentos = [...this.medicamentos, result];
          this._snackBar.open('Medicamento cadastrado com sucesso', 'X', {
            duration: 3000,
          });
          this.medicamentoService.cadastrar(result).pipe(take(1)).subscribe();
        }
      }
    });
  }

  deletarMedicamento(medicamento: Medicamento) {
    if (confirm('Tem certeza que deseja excluir?')) {
      this.medicamentos = this.medicamentos.filter(
        (med) => med.medicamentoId !== medicamento.medicamentoId
      );
      this.medicamentoService
        .deletar(medicamento.medicamentoId)
        .pipe(take(1))
        .subscribe();
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
