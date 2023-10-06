import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';
import { faMagnifyingGlass, faTrash, faEdit, faPlus } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { CadastrarTutorComponent } from './cadastrar-tutor/cadastrar-tutor.component';
import { EditarTutorComponent } from './editar-tutor/editar-tutor.component';
import { DialogConfirmacaoComponent } from '../dialog-confirmacao/dialog-confirmacao.component';
import { DetalhesTutorComponent } from './detalhes-tutor/detalhes-tutor.component';

@Component({
  selector: 'app-tutor',
  templateUrl: './tutor.component.html',
  styleUrls: ['./tutor.component.scss']
})
export class TutorComponent implements OnInit {
  tutores: Tutor[] = [];
  faMagnfyingGlass = faMagnifyingGlass;
  faTrash = faTrash;
  faEdit = faEdit;
  faPlus = faPlus;

  constructor(
    private tutorService: TutorService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.tutorService.getAllTutores().subscribe((response: any) => {
      if (response && response.data) {
        this.tutores = response.data;
      } else {
        this.tutores = [];
      }
    });
  }
  openDialogDetalhes(tutor: Tutor): void{
    const dialogRef = this.dialog.open(DetalhesTutorComponent,{
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data:tutor
    });
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(CadastrarTutorComponent,{
      autoFocus: false,
      maxHeight: '90vh',
      width: '1250px'
    });

    dialogRef.afterClosed().subscribe((result:any) => {

    });
  }
  openDialogEdit(tutor: Tutor): void{
    const dialogRef = this.dialog.open(EditarTutorComponent,{
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data:tutor
    });
  }
  deletarTutor(tutor:Tutor){
    const dialogRef = this.dialog.open(DialogConfirmacaoComponent,{
      data:"Tem certeza que quer deletar o tutor com o nome: " + tutor.nome_normalizado + " ?"
    });

    dialogRef.afterClosed().subscribe((result:boolean) => {
      if(result){
        const obs = {
          next: (msg: string) => {
            this.toastr.success(msg)
          },
          error: (err: any) => {
            err.forEach((error: any) => {
              this.toastr.error(error);
            });
          },
        };
        this.tutorService.deletar(tutor.id).subscribe(obs);
      }
    });
  }
}
