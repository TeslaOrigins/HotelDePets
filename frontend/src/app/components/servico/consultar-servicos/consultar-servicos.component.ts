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
import { Servico } from 'src/app/models/Servico';
import { ServicoService } from 'src/app/services/servico.service';

@Component({
  selector: 'app-consultar-servicos',
  templateUrl: './consultar-servicos.component.html',
  styleUrls: ['./consultar-servicos.component.scss'],
})
export class ConsultarServicosComponent {
  servicos: Servico[] = [];
  displayedColumns: string[] = ['nome', 'descricao', 'preco'];

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private servicoService: ServicoService
  ) {
    this.servicoService
      .getAllServicos()
      .pipe(take(1))
      .subscribe((serv) => (this.servicos = serv));
  }

  openDialog(servico?: Servico): void {
    const dialogRef = this.dialog.open(DialogServico, {
      data: { servico: servico },
    });

    dialogRef.afterClosed().subscribe((result: Servico) => {
      if (result) {
        if (result.servicoId !== null) {
          const index = this.servicos.findIndex(
            (serv) => serv.servicoId === result.servicoId
          );
          this.servicos = [
            ...this.servicos.slice(0, index),
            result,
            ...this.servicos.slice(index + 1),
          ];
          this._snackBar.open('Serviço Atualizado com sucesso', 'X', {
            duration: 3000,
          });
          this.servicoService.editar(result).pipe(take(1)).subscribe();
        } else {
          this.servicos = [...this.servicos, result];
          this._snackBar.open('Serviço cadastrado com sucesso', 'X', {
            duration: 3000,
          });
          this.servicoService.cadastrar(servico).pipe(take(1)).subscribe();
        }
      }
    });
  }

  deletarServico(servico: Servico) {
    if (confirm('Tem certeza que deseja excluir?')) {
      this.servicos = this.servicos.filter(
        (serv) => serv.servicoId !== servico.servicoId
      );
      this.servicoService.deletar(servico.servicoId).pipe(take(1)).subscribe();
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
