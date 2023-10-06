import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pet } from 'src/app/models/Pet';
import { PetService } from 'src/app/services/pet.service';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-editar-pet',
  templateUrl: './editar-pet.component.html',
  styleUrls: ['./editar-pet.component.scss']
})
export class EditarPetComponent implements OnInit {
  petForm: FormGroup;
  faPlus = faPlus;
  petId: number;

  constructor(
    private builder: FormBuilder,
    private petService: PetService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute,
    public dialogRef: MatDialogRef<EditarPetComponent>
  ) {
    this.petId = 0;
    this.petForm = this.builder.group({
      petId: [0],
      nome: new FormControl<string>('', Validators.required),
      idade_mes: new FormControl<number>(0, Validators.min(0)),
      raca: new FormControl<string>(''),
      sexo: new FormControl<string>(''),
      peso: new FormControl<number>(0, Validators.min(0)),
      especie: new FormControl<string>('')
    });
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const id = +params['id']; 
      if (!isNaN(id)) {
        this.petId = id;
        this.petService.getPetById(this.petId).subscribe((pet: Pet) => {
          this.petForm.patchValue({
            id: pet.id,
            nome: pet.nome,
            idade_mes: pet.idade_mes,
            raca: pet.raca,
            sexo: pet.sexo,
            peso: pet.peso,
            especie: pet.especie
          });
        });
      } else {
        console.error('ID inválido:', params['id']);
      }
    });
  }

  editar() {
    if (this.petForm.valid) {
      const obj = {
        petId: this.petForm.controls['petId'].value,
        nome: this.petForm.controls['nome'].value,
        idadeMes: this.petForm.controls['idade_mes'].value,
        raca: this.petForm.controls['raca'].value,
        sexo: this.petForm.controls['sexo'].value,
        peso: this.petForm.controls['peso'].value,
        especie: this.petForm.controls['especie'].value,
      };

      this.petService.editar(obj.petId, obj).subscribe(
        (petEditado: Pet) => {
          console.log('Pet editado:', petEditado);
          
          this.toastr.success('Pet editado com sucesso');
          this.router.navigateByUrl(`/pets/detalhes/${petEditado.id}`);
          this.dialogRef.close();
        },
        (error: any) => {
          console.error('Erro ao editar pet:', error);
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