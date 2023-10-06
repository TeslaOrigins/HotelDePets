import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Hospedagem } from 'src/app/models/Hospedagem'; // Certifique-se de importar o modelo de Hospedagem correto
import { HospedagemService } from 'src/app/services/hospedagem.service'; // Certifique-se de importar o serviço correto
import { faMagnifyingGlass, faTrash, faEdit, faPlus } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { CadastrarHospedagemComponent } from './cadastrar-hospedagem/cadastrar-hospedagem.component'; // Certifique-se de importar o componente correto
import { EditarHospedagemComponent } from './editar-hospedagem/editar-hospedagem.component'; // Certifique-se de importar o componente correto
import { DialogConfirmacaoComponent } from '../dialog-confirmacao/dialog-confirmacao.component';
import { DetalhesHospedagemComponent } from './detalhes-hospedagem/detalhes-hospedagem.component'; // Certifique-se de importar o componente correto

@Component({
  selector: 'app-hospedagem',
  templateUrl: './hospedagem.component.html',
  styleUrls: ['./hospedagem.component.scss']
})
export class HospedagemComponent implements OnInit {
  hospedagens: Hospedagem[] = [];
  faMagnfyingGlass = faMagnifyingGlass;
  faTrash = faTrash;
  faEdit = faEdit;
  faPlus = faPlus;
  hospedagens$: Observable<Hospedagem[]>;

  constructor(
    private hospedagemService: HospedagemService, // Certifique-se de injetar o serviço correto
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {
    this.hospedagens$ = hospedagemService.getAllHospedagens();
    const obs = {
      next: (hospedagens: Hospedagem[]) => {
        this.hospedagens = hospedagens;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          toastr.error(error);
        });
      },
    };
    this.hospedagens$.subscribe(obs);
  }

  getAllHospedagens() {
    const obs = {
      next: (hospedagens: Hospedagem[]) => {
        this.hospedagens = hospedagens;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          this.toastr.error(error);
        });
      },
    };
    this.hospedagens$.subscribe(obs);
  }

  ngOnInit() {}

  openDialogDetalhes(hospedagem: Hospedagem): void {
    const dialogRef = this.dialog.open(DetalhesHospedagemComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: hospedagem
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllHospedagens();
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(CadastrarHospedagemComponent, {
      autoFocus: false,
      maxHeight: '90vh',
      width: '1250px'
    });

    dialogRef.afterClosed().subscribe((result: any) => {});
  }

  openDialogEdit(hospedagem: Hospedagem): void {
    const dialogRef = this.dialog.open(EditarHospedagemComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: hospedagem
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllHospedagens();
    });
  }

  deletarHospedagem(hospedagem: Hospedagem) {
    const dialogRef = this.dialog.open(DialogConfirmacaoComponent, {
      data: 'Tem certeza que quer deletar a hospedagem com ID: ' + hospedagem.hospedagemId + ' ?'
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
        this.hospedagemService.deletar(hospedagem.hospedagemId).subscribe(obs);
      }
    });
  }
}