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
import { Servico } from 'src/app/models/Servico';

@Component({
  selector: 'app-consultar-servicos',
  templateUrl: './consultar-servicos.component.html',
  styleUrls: ['./consultar-servicos.component.scss'],
})
export class ConsultarServicosComponent {
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
  displayedColumns: string[] = ['nome', 'descricao', 'preco'];

  constructor(public dialog: MatDialog, private _snackBar: MatSnackBar) {}

  openDialog(servico?: Servico): void {
    const dialogRef = this.dialog.open(DialogServico, {
      data: { servico: servico },
    });

    dialogRef.afterClosed().subscribe((result: Servico) => {
      if (result) {
        if (result.servicoId !== null) {
          const index = this.mockServicos.findIndex(
            (serv) => serv.servicoId === result.servicoId
          );
          this.mockServicos = [
            ...this.mockServicos.slice(0, index),
            result,
            ...this.mockServicos.slice(index + 1),
          ];
          this._snackBar.open('Serviço Atualizado com sucesso', 'X', {
            duration: 3000,
          });
        } else {
          this.mockServicos = [...this.mockServicos, result];
          this._snackBar.open('Serviço cadastrado com sucesso', 'X', {
            duration: 3000,
          });
        }
      }
    });
  }

  deletarServico(servico: Servico) {
    if (confirm('Tem certeza que deseja excluir?')) {
      this.mockServicos = this.mockServicos.filter(
        (serv) => serv.servicoId !== servico.servicoId
      );
    }
  }
  editarServico(servico: Servico) {
    this.openDialog(servico);
  }
}

@Component({
  selector: 'dialog-servico',
  templateUrl: '../dialog-servico.component.html',
})
export class DialogServico {
  servicoForm: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<DialogServico>,
    @Inject(MAT_DIALOG_DATA) public data: { servico: Servico },
    private builder: FormBuilder
  ) {
    const servico = data.servico;
    this.servicoForm = this.builder.group({
      servicoId: new FormControl<number>(servico?.servicoId),
      nome: new FormControl<string>(servico?.nome, Validators.required),
      descricao: new FormControl<string>(
        servico?.descricao,
        Validators.required
      ),
      preco: new FormControl<number>(servico?.preco, Validators.required),
    });
  }

  onSubmit(): void {
    if (this.servicoForm.valid)
      this.dialogRef.close(Object.assign(this.servicoForm.value));
  }

  close(): void {
    this.dialogRef.close();
  }
}
