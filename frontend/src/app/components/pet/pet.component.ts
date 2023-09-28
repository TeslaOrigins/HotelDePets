import { Component, Inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
import { Pet } from 'src/app/models/Pet';
import { DetalhesPetComponent } from './detalhes-pet/detalhes-pet.component';
import { Observable } from 'rxjs';
import { PetService } from 'src/app/services/pet.service';
import { Tutor } from 'src/app/models/Tutor';
import { ToastrService } from 'ngx-toastr';
import { CadastrarPetComponent } from './cadastrar-pet/cadastrar-pet.component';

import { DialogConfirmacaoComponent } from '../dialog-confirmacao/dialog-confirmacao.component';
import { AlterarPetComponent } from './alterar-pet/alterar-pet.component';

@Component({
  selector: 'app-pet',
  templateUrl: './pet.component.html',
  styleUrls: ['./pet.component.scss'],
})
export class PetComponent {
  pets: Pet[] = [];
  pets$: Observable<Pet[]>;
  displayedColumns: string[] = [
    'fotoUrl',
    'nome',
    'idadeMes',
    'peso',
    'sexo',
    'raca',
    'especie',
  ];

  constructor(
    public dialog: MatDialog,
    private petService: PetService,
    private toastr: ToastrService
  ) {
    this.pets$ = petService.getAllPets();
    const obs = {
      next: (pets: Pet[]) => {
        this.pets = pets;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          toastr.error(error);
        });
      },
    };
    this.pets$.subscribe(obs);
  }
  getAllPets() {
    const obs = {
      next: (Pets: Pet[]) => {
        this.pets = Pets;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          this.toastr.error(error);
        });
      },
    };
    this.pets$.subscribe(obs);
  }
  ngOnInit() {}
  openDialogDetalhes(pet: Pet): void {
    const dialogRef = this.dialog.open(DetalhesPetComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: pet,
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllPets();
    });
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(CadastrarPetComponent, {
      autoFocus: false,
      maxHeight: '90vh',
      width: '1250px',
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllPets();
    });
  }
  openDialogEdit(pet: Pet): void {
    const dialogRef = this.dialog.open(AlterarPetComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: pet,
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      this.getAllPets();
    });
  }
  apagarTutor(pet: Pet) {
    const dialogRef = this.dialog.open(DialogConfirmacaoComponent, {
      data: 'Tem certeza que quer apagar o pet com o nome: ' + pet.nome + ' ?',
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
        this.petService.apagar(pet.petId).subscribe(obs);
      }
    });
  }
}
