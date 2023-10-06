import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';
import {
  faMagnifyingGlass,
  faTrash,
  faEdit,
  faPlus,
} from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { CadastrarTutorComponent } from './cadastrar-tutor/cadastrar-tutor.component';
import { AlterarTutorComponent } from './alterar-tutor/alterar-tutor.component';
import { DialogConfirmacaoComponent } from '../dialog-confirmacao/dialog-confirmacao.component';
import { DetalhesTutorComponent } from './detalhes-tutor/detalhes-tutor.component';
@Component({
  selector: 'app-tutor',
  templateUrl: './tutor.component.html',
  styleUrls: ['./tutor.component.scss'],
})
export class TutorComponent implements OnInit {
  tutores: Tutor[] = [];
  faMagnfyingGlass = faMagnifyingGlass;
  faTrash = faTrash;
  faEdit = faEdit;
  faPlus = faPlus;
  tutores$: Observable<Tutor[]>;
  constructor(
    private tutorService: TutorService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {
    this.tutores$ = tutorService.getAllTutores();
    const obs = {
      next: (tutores: Tutor[]) => {
        this.tutores = tutores;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          toastr.error(error);
        });
      },
    };
    this.tutores$.subscribe(obs);
  }
  getAllTutores() {
    const obs = {
      next: (tutores: Tutor[]) => {
        this.tutores = tutores;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          this.toastr.error(error);
        });
      },
    };
    this.tutores$.subscribe(obs);
  }
  ngOnInit() {}
  openDialogDetalhes(tutor: Tutor): void {
    const dialogRef = this.dialog.open(DetalhesTutorComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: tutor,
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllTutores();
    });
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(CadastrarTutorComponent, {
      autoFocus: false,
      maxHeight: '90vh',
      width: '1250px',
    });

    dialogRef.afterClosed().subscribe((result: Tutor) => {
      if (result)
        this.tutores = [
          ...this.tutores,
          { ...result, nomeNormalizado: result.nome },
        ];
    });
  }
  openDialogEdit(tutor: Tutor): void {
    const dialogRef = this.dialog.open(AlterarTutorComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: tutor,
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllTutores();
    });
  }
  apagarTutor(tutor: Tutor) {
    const dialogRef = this.dialog.open(DialogConfirmacaoComponent, {
      data:
        'Tem certeza que quer apagar o tutor com o nome: ' +
        tutor.nomeNormalizado +
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
        this.tutores = this.tutores.filter(
          (tut) => tut.tutorId !== tutor.tutorId
        );
        this.tutorService.apagar(tutor.tutorId).subscribe(obs);
      }
    });
  }
}
