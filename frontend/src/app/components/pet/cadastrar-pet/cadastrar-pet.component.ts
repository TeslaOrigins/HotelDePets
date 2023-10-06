import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PetService } from 'src/app/services/pet.service';
import { TutorService } from 'src/app/services/tutor.service';
import { Pet } from 'src/app/models/Pet';
import { Tutor } from 'src/app/models/Tutor';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-cadastrar-pet',
  templateUrl: './cadastrar-pet.component.html',
  styleUrls: ['./cadastrar-pet.component.scss']
})
export class CadastrarPetComponent implements OnInit {
  petForm: FormGroup;
  faPlus = faPlus;
  tutors: Tutor[] = [];

  constructor(
    private builder: FormBuilder,
    private petService: PetService,
    private tutorService: TutorService,
    private toastr: ToastrService,
    private router: Router,
    public dialogRef: MatDialogRef<CadastrarPetComponent>
  ) {
    this.petForm = this.builder.group({
      nome: new FormControl<string>('', Validators.required),
      idade_mes: new FormControl<number>(0, [Validators.required, Validators.min(1)]),
      raca: new FormControl<string>('', Validators.required),
      sexo: new FormControl<string>('', Validators.required),
      peso: new FormControl<number>(0, [Validators.required, Validators.min(0.01)]),
      especie: new FormControl<string>('', Validators.required),
      tutorId: new FormControl<number>(0, Validators.required)
    });
  }

  ngOnInit() {
    // Carregar a lista de tutores para preencher o dropdown ou outro componente de seleção
    this.tutorService.getAllTutores().subscribe((tutors: Tutor[]) => {
      this.tutors = tutors;
    });
  }

  cadastrar() {
    if (this.petForm.valid) {
      const obj = {
        nome: this.petForm.controls['nome'].value,
        idade_mes: this.petForm.controls['idade_mes'].value,
        raca: this.petForm.controls['raca'].value,
        sexo: this.petForm.controls['sexo'].value,
        peso: this.petForm.controls['peso'].value,
        especie: this.petForm.controls['especie'].value,
        tutor_id: this.petForm.controls['tutorId'].value,
      };

      // Chama o método de cadastro do PetService
      this.petService.cadastrar(obj).subscribe(
        (response: any) => {
          if (response && response.data) {
            // Continua o tratamento de sucesso do pet
            this.toastr.success('Pet cadastrado com sucesso');
            this.router.navigateByUrl('/pets/');
            this.dialogRef.close();
          }
        },
        (error: any) => {
          console.error('Erro ao cadastrar pet:', error);
          // Trate os erros relacionados ao cadastro do pet aqui
          if (error.status == 400) {
            error.error.forEach((element: string) => {
              this.toastr.error(element);
            });
          } else {
            this.toastr.error(error.error);
          }
          this.dialogRef.close();
        }
      );
    }
  }
}