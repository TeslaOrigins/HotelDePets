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
      petId: [0], // Inclua o petId aqui com um valor inicial de 0
      nome: new FormControl<string>('', Validators.required),
      idade_mes: new FormControl<number>(0, Validators.min(0)), // Defina o valor inicial conforme necessário
      raca: new FormControl<string>(''),
      sexo: new FormControl<string>(''),
      peso: new FormControl<number>(0, Validators.min(0)), // Defina o valor inicial conforme necessário
      especie: new FormControl<string>('')
    });
  }

  ngOnInit() {
    // Obtenha o ID do pet da rota
    this.route.params.subscribe(params => {
      const id = +params['id']; // Certifique-se de converter para número
      if (!isNaN(id)) {
        this.petId = id;
        // Agora você pode fazer a chamada para o serviço com this.petId
        this.petService.getPetById(this.petId).subscribe((pet: Pet) => {
          // Preencha os campos do formulário com os dados do pet
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
        // Lide com o cenário em que o ID não é um número válido
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

      // Chama o método de edição do PetService
      this.petService.editar(obj.petId, obj).subscribe(
        (petEditado: Pet) => {
          // Pet editado com sucesso
          console.log('Pet editado:', petEditado);
          
          // Continue o tratamento de sucesso do pet
          this.toastr.success('Pet editado com sucesso');
          this.router.navigateByUrl(`/pets/detalhes/${petEditado.id}`);
          this.dialogRef.close();
        },
        (error: any) => {
          console.error('Erro ao editar pet:', error);
          // Trate os erros relacionados à edição do pet aqui
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