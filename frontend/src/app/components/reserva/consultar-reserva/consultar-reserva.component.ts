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
import { Reserva } from 'src/app/models/Reserva';
import { Servico } from 'src/app/models/Servico';
import { ReservaService } from 'src/app/services/reserva.service';

@Component({
  selector: 'app-consultar-reserva',
  templateUrl: './consultar-reserva.component.html',
  styleUrls: ['./consultar-reserva.component.scss'],
})
export class ConsultarReservaComponent {
  reservas: Reserva[] = [];

  displayedColumns: string[] = ['dataCheckin', 'dataCheckout', 'servicos'];

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private reservaService: ReservaService
  ) {
    this.reservaService
      .getAllreservas()
      .pipe(take(1))
      .subscribe((reservas) => (this.reservas = reservas));
  }

  getServicoNames(servicos: Servico[]) {
    return servicos.map((servico) => servico.nome).join(',');
  }

  openDialog(reserva?: Reserva): void {
    const dialogRef = this.dialog.open(DialogReserva, {
      data: { reserva: reserva },
    });

    dialogRef.afterClosed().subscribe((result: Reserva) => {
      if (result) {
        if (result.reservaId !== null) {
          const index = this.reservas.findIndex(
            (serv) => serv.reservaId === result.reservaId
          );
          this.reservas = [
            ...this.reservas.slice(0, index),
            result,
            ...this.reservas.slice(index + 1),
          ];
          this._snackBar.open('Reserva Atualizado com sucesso', 'X', {
            duration: 3000,
          });
          this.reservaService.editar(result).pipe(take(1)).subscribe();
        } else {
          this.reservas = [...this.reservas, result];
          this._snackBar.open('Reserva cadastrado com sucesso', 'X', {
            duration: 3000,
          });
          this.reservaService.cadastrar(result).pipe(take(1)).subscribe();
        }
      }
    });
  }

  deletarReserva(reserva: Reserva) {
    if (confirm('Tem certeza que deseja excluir?')) {
      this.reservas = this.reservas.filter(
        (serv) => serv.reservaId !== reserva.reservaId
      );
      this.reservaService.deletar(reserva.reservaId).pipe(take(1)).subscribe();
    }
  }
  editarReserva(reserva: Reserva) {
    this.openDialog(reserva);
  }
}

@Component({
  selector: 'dialog-reserva',
  templateUrl: '../dialog-reserva.component.html',
})
export class DialogReserva {
  reservaForm: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<DialogReserva>,
    @Inject(MAT_DIALOG_DATA) public data: { reserva: Reserva },
    private builder: FormBuilder
  ) {
    const reserva = data.reserva;
    this.reservaForm = this.builder.group({
      reservaId: new FormControl<number>(reserva?.reservaId),
      dataCheckin: new FormControl<Date>(
        reserva?.dataCheckin,
        Validators.required
      ),
      dataCheckout: new FormControl<Date>(
        reserva?.dataCheckout,
        Validators.required
      ),
      servicos: new FormControl<Servico[]>(reserva?.servicos),
    });
  }

  mockServicos: Servico[] = [
    { servicoId: 0, nome: 'tosa', descricao: 'tosa do pet', preco: 80 },
    { servicoId: 1, nome: 'banho', descricao: 'banho do pet', preco: 50 },
    {
      servicoId: 2,
      nome: 'cortar unha',
      descricao: 'corta unha do pet',
      preco: 20,
    },
    {
      servicoId: 3,
      nome: 'passear',
      descricao: 'leva o pet para passear',
      preco: 5,
    },
  ];

  onSubmit(): void {
    if (this.reservaForm.valid)
      this.dialogRef.close(Object.assign(this.reservaForm.value));
  }

  close(): void {
    this.dialogRef.close();
  }
}
