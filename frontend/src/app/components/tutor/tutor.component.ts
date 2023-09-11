import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';
import { faMagnifyingGlass,faTrash,faEdit,faPlus } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { CadastrarTutorComponent } from './cadastrar-tutor/cadastrar-tutor.component';
import { EditarTutorComponent } from './editar-tutor/editar-tutor.component';
@Component({
  selector: 'app-tutor',
  templateUrl: './tutor.component.html',
  styleUrls: ['./tutor.component.scss']
})
export class TutorComponent implements OnInit {
  tutores: Tutor[] =[];
  faMagnfyingGlass = faMagnifyingGlass;
  faTrash = faTrash;
  faEdit = faEdit;
  faPlus = faPlus;
  tutores$ : Observable<Tutor[]>;
  constructor(private tutorService: TutorService,
              private toastr: ToastrService,
              public dialog: MatDialog) {
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

  ngOnInit() {
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

    dialogRef.afterClosed().subscribe((result:any) => {
    });
  }
  deletarTutor(){

  }

}
