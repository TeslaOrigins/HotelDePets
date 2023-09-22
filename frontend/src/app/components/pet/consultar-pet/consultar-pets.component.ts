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

@Component({
  selector: 'app-consultar-pets',
  templateUrl: './consultar-pets.component.html',
  styleUrls: ['./consultar-pets.component.scss'],
})
export class ConsultarPetsComponent {
  mockPets: Pet[] = [
    {
      petId: 1,
      nome: 'juba',
      idadeMes: 3,
      raca: 'nobre',
      sexo: 'Masculino',
      fotoUrl:
        'https://img.freepik.com/free-photo/puppy-that-is-walking-snow_1340-37228.jpg',
      peso: 30,
      especie: 'nobreza',
    },
    {
      petId: 2,
      nome: 'jujuba',
      idadeMes: 6,
      raca: 'nobre',
      sexo: 'Feminino',
      fotoUrl:
        'https://img.freepik.com/free-photo/puppy-that-is-walking-snow_1340-37228.jpg',
      peso: 60,
      especie: 'nobreza',
    },
    {
      petId: 2,
      nome: 'raimundo',
      idadeMes: 12,
      raca: 'ruim',
      sexo: 'Masculino',
      fotoUrl:
        'https://images.unsplash.com/photo-1598875706250-21faaf804361?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8NHx8fGVufDB8fHx8fA%3D%3D&w=1000&q=80',
      peso: 10,
      especie: 'vira-lata',
    },
  ];
  displayedColumns: string[] = [
    'fotoUrl',
    'nome',
    'idadeMes',
    'peso',
    'sexo',
    'raca',
    'especie',
  ];

  constructor(public dialog: MatDialog) {}

  openDialog(pet?: Pet): void {
    const dialogRef = this.dialog.open(DialogPet);

    dialogRef.afterClosed().subscribe((result) => {
      console.log(result);
      if (result) this.mockPets = [...this.mockPets, result];
    });
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
    @Inject(MAT_DIALOG_DATA) public data: Pet,
    private builder: FormBuilder
  ) {
    this.petForm = this.builder.group({
      nome: new FormControl<string>('', Validators.required),
      idadeMes: new FormControl<number>(0, Validators.required),
      raca: new FormControl<string>('', Validators.required),
      especie: new FormControl<string>('', Validators.required),
      peso: new FormControl<number>(0, Validators.required),
      sexo: new FormControl<string>('', Validators.required),
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
