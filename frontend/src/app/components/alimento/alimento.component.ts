import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { faMagnifyingGlass, faTrash, faEdit, faPlus } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Alimento } from 'src/app/models/Alimento';
import { DialogConfirmacaoComponent } from '../dialog-confirmacao/dialog-confirmacao.component';
import { AlterarAlimentoComponent } from './alterar-alimento/alterar-alimento.component';
import { CadastrarAlimentoComponent } from './cadastrar-alimento/cadastrar-alimento.component';
import { DetalhesAlimentoComponent } from './detalhes-alimento/detalhes-alimento.component';
import { AlimentoService } from 'src/app/services/alimento.service';

@Component({
  selector: 'app-alimento',
  templateUrl: './alimento.component.html',
  styleUrls: ['./alimento.component.css']
})
export class AlimentoComponent implements OnInit {

  alimentos: Alimento[] = [];
  faMagnfyingGlass = faMagnifyingGlass;
  faTrash = faTrash;
  faEdit = faEdit;
  faPlus = faPlus;
  alimentos$: Observable<Alimento[]>;
  constructor(
    private alimentoService: AlimentoService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {
    this.alimentos$ = alimentoService.getAllAlimentos();
    const obs = {
      next: (Alimentoes: Alimento[]) => {
        this.alimentos = Alimentoes;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          toastr.error(error);
        });
      },
    };
    this.alimentos$.subscribe(obs);
  }
  getAllAlimentos() {
    const obs = {
      next: (Alimentoes: Alimento[]) => {
        this.alimentos = Alimentoes;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          this.toastr.error(error);
        });
      },
    };
    this.alimentos$.subscribe(obs);
  }
  ngOnInit() {}
  openDialogDetalhes(Alimento: Alimento): void {
    const dialogRef = this.dialog.open(DetalhesAlimentoComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: Alimento,
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllAlimentos();
    });
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(CadastrarAlimentoComponent, {
      autoFocus: false,
      maxHeight: '90vh',
      width: '1250px',
    });

    dialogRef.afterClosed().subscribe((result: any) => {});
  }
  openDialogEdit(Alimento: Alimento): void {
    const dialogRef = this.dialog.open(AlterarAlimentoComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: Alimento,
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllAlimentos();
    });
  }
  apagarAlimento(Alimento: Alimento) {
    const dialogRef = this.dialog.open(DialogConfirmacaoComponent, {
      data:
        'Tem certeza que quer apagar o Alimento com o nome: ' +
        Alimento.nome +
        ' ?',
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        const obs = {
          next: (msg: string) => {
            this.toastr.success(msg);
          },
          error: (err: any) => {
            err.forEach((error: any) => {
              this.toastr.error(error);
            });
          },
        };
        this.alimentoService.apagar(Alimento.alimentoId).subscribe(obs);
      }
    });
  }
}
