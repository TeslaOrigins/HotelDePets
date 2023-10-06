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
import { MatSnackBar } from '@angular/material/snack-bar';
import { take } from 'rxjs';
import { Pet } from 'src/app/models/Pet';
import { PetService } from 'src/app/services/pet.service';

@Component({
  selector: 'app-consultar-pets',
  templateUrl: './consultar-pets.component.html',
  styleUrls: ['./consultar-pets.component.scss'],
})
export class ConsultarPetsComponent {
  pets: Pet[] = [];
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
    private _snackBar: MatSnackBar
  ) {
    this.petService
      .getAllPets()
      .pipe(take(1))
      .subscribe((pets) => (this.pets = pets));
  }

  openDialog(pet?: Pet): void {
    const dialogRef = this.dialog.open(DialogPet, {
      data: { pet },
    });

    dialogRef.afterClosed().subscribe((result: Pet) => {
      if (result) {
        if (result.petId !== null) {
          const index = this.pets.findIndex(
            (pet) => pet.petId === result.petId
          );
          this.pets = [
            ...this.pets.slice(0, index),
            result,
            ...this.pets.slice(index + 1),
          ];
          this._snackBar.open('Serviço Atualizado com sucesso', 'X', {
            duration: 3000,
          });
          this.petService.alterar(result).pipe(take(1)).subscribe();
        } else {
          this.pets = [...this.pets, result];
          this._snackBar.open('Serviço cadastrado com sucesso', 'X', {
            duration: 3000,
          });
          this.petService.cadastrar(pet).pipe(take(1)).subscribe();
        }
      }
    });
  }

  deletarPet(pet: Pet) {
    if (confirm('Tem certeza que deseja excluir?')) {
      this.pets = this.pets.filter((pets) => pets.petId !== pet.petId);
      this.petService.apagar(pet.petId).pipe(take(1)).subscribe();
    }
  }
  editarPet(pet: Pet) {
    this.openDialog(pet);
  }
}

@Component({
  selector: 'dialog-pet',
  templateUrl: '../dialog-pet.component.html',
})
export class DialogPet {
  petForm: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<DialogPet>,
    @Inject(MAT_DIALOG_DATA) public data: { pet: Pet },
    private builder: FormBuilder
  ) {
    const pet = data.pet;
    this.petForm = this.builder.group({
      petId: new FormControl<number>(pet?.petId),
      nome: new FormControl<string>(pet?.nome, Validators.required),
      idadeMes: new FormControl<number>(pet?.idadeMes, Validators.required),
      raca: new FormControl<string>(pet?.raca, Validators.required),
      especie: new FormControl<string>(pet?.especie, Validators.required),
      peso: new FormControl<number>(pet?.peso, Validators.required),
      sexo: new FormControl<string>(pet?.sexo, Validators.required),
    });
  }

  onSubmit(): void {
    if (this.petForm.valid)
      this.dialogRef.close(Object.assign(this.petForm.value));
  }

  close(): void {
    this.dialogRef.close();
  }
}
