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
import { Reserva } from 'src/app/models/Reserva';
import { Servico } from 'src/app/models/Servico';

@Component({
  selector: 'app-consultar-reserva',
  templateUrl: './consultar-reserva.component.html',
  styleUrls: ['./consultar-reserva.component.scss'],
})
export class ConsultarReservaComponent {
  mockReservas: Reserva[] = [
    {
      reservaId: 0,
      dataCheckin: new Date('2023-10-04'),
      dataCheckout: new Date('2023-10-08'),
      servicos: [
        { servicoId: 0, nome: 'tosa', descricao: 'tosa do pet', preco: 80 },
        { servicoId: 1, nome: 'banho', descricao: 'banho do pet', preco: 50 },
      ],
    },
    {
      reservaId: 1,
      dataCheckin: new Date('2023-10-05'),
      dataCheckout: new Date('2023-10-09'),
      servicos: [
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
      ],
    },
    {
      reservaId: 2,
      dataCheckin: new Date('2023-10-06'),
      dataCheckout: new Date('2023-11-01'),
      servicos: [
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
      ],
    },
  ];

  displayedColumns: string[] = ['dataCheckin', 'dataCheckout', 'servicos'];

  constructor(public dialog: MatDialog, private _snackBar: MatSnackBar) {}

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
          const index = this.mockReservas.findIndex(
            (serv) => serv.reservaId === result.reservaId
          );
          this.mockReservas = [
            ...this.mockReservas.slice(0, index),
            result,
            ...this.mockReservas.slice(index + 1),
          ];
          this._snackBar.open('Reserva Atualizado com sucesso', 'X', {
            duration: 3000,
          });
        } else {
          this.mockReservas = [...this.mockReservas, result];
          this._snackBar.open('Reserva cadastrado com sucesso', 'X', {
            duration: 3000,
          });
        }
      }
    });
  }

  deletarReserva(reserva: Reserva) {
    if (confirm('Tem certeza que deseja excluir?')) {
      this.mockReservas = this.mockReservas.filter(
        (serv) => serv.reservaId !== reserva.reservaId
      );
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
