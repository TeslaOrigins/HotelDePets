import { Component, Inject, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { ToastrService } from 'ngx-toastr';
import { Alimento } from 'src/app/models/Alimento';
import { Pet } from 'src/app/models/Pet';
import { PetService } from 'src/app/services/pet.service';
import { CadastrarPetComponent } from '../cadastrar-pet/cadastrar-pet.component';
import { Dieta } from 'src/app/models/Dieta';

@Component({
  selector: 'app-alterar-pet',
  templateUrl: './alterar-pet.component.html',
  styleUrls: ['./alterar-pet.component.css'],
})
export class AlterarPetComponent implements OnInit {
  petForm: FormGroup;
  alimentos: Alimento[] = [
    {
      alimentoId: 1,
      dataEntrada: new Date(),
      nome: 'whiskas velha 600gr',
      precoReabastecimento: 2.0,
      quantidadeEstoque: 2,
    },
    {
      alimentoId: 2,
      dataEntrada: new Date(),
      nome: 'whiskas nova 600gr',
      precoReabastecimento: 2.0,
      quantidadeEstoque: 2,
    },
  ];

  constructor(
    private petService: PetService,
    public dialogRef: MatDialogRef<AlterarPetComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Pet,
    private builder: FormBuilder,
    private toastr: ToastrService
  ) {
    this.petForm = this.builder.group({
      nome: new FormControl<string>('', Validators.required),
      idadeMes: new FormControl<number>(0, Validators.required),
      raca: new FormControl<string>('', Validators.required),
      especie: new FormControl<string>('', Validators.required),
      peso: new FormControl<number>(0, Validators.required),
      sexo: new FormControl<string>('', Validators.required),
      veterinarioNome: new FormControl<string>(''),
      veterinarioTelefone: new FormControl<string>(''),
      dieta: this.builder.array([], Validators.required),
    });
  }
  adicionarAlimento(i: number, evento: MatSelectChange) {
    if (evento.value != null) {
      // Coloca o alimento na lista de alimentos
      this.dieta.at(i).controls['alimentos'].value.push(evento.value);

      // retira dos disponiveis
      const index = this.dieta
        .at(i)
        .controls['alimentosDisponiveis'].value.indexOf(evento.value, 0);

      if (index > -1) {
        this.dieta
          .at(i)
          .controls['alimentosDisponiveis'].value.splice(index, 1);
      }

      this.dieta.at(i).updateValueAndValidity();
    } else {
      this.toastr.error('Não é possivel adicionar um alimento não existente');
    }
  }
  removerAlimento(i: number, alimento: Alimento) {
    // remove da lista de alimentos
    const index = this.dieta
      .at(i)
      .controls['alimentos'].value.indexOf(alimento, 0);

    if (index > -1) {
      this.dieta.at(i).controls['alimentos'].value.splice(index, 1);
    }
    //adiciona na lista de alimentos disponiveis
    this.dieta.at(i).controls['alimentosDisponiveis'].value.push(alimento);
  }
  onSubmit(): void {
    if (this.petForm.valid)
      this.dialogRef.close(Object.assign(this.petForm.value));
  }

  close(): void {
    this.dialogRef.close();
  }
  get dieta() {
    return this.petForm.controls['dieta'] as FormArray<FormGroup>;
  }

  pushNovodieta() {
    const dietaForm = this.builder.group({
      horarioAlimentacao: new FormControl<Date>(
        new Date(0),
        Validators.required
      ),
      quantidade: new FormControl<number>(0, Validators.required),
      observacoes: new FormControl<string>(''),
      alimentosDisponiveis: new FormControl<Alimento[]>(this.alimentos),
      alimentos: new FormControl<Alimento[]>([], Validators.required),
    });
    this.dieta.push(dietaForm);
  }
  pushNovodietaExistente(dieta: Dieta) {
    const dietaForm = this.builder.group({
      horarioAlimentacao: new FormControl<Date>(
        dieta.horarioAlimentacao,
        Validators.required
      ),
      quantidade: new FormControl<number>(
        dieta.quantidade,
        Validators.required
      ),
      observacoes: new FormControl<string>(dieta.observacoes),
      // alimentos que não estão na dieta
      alimentosDisponiveis: new FormControl<Alimento[]>(
        this.alimentos.filter((al) => dieta.alimentos.indexOf(al) != -1)
      ),
      alimentos: new FormControl<Alimento[]>([], Validators.required),
    });
    this.dieta.push(dietaForm);
  }

  deletedieta(dietaIndex: number) {
    this.dieta.removeAt(dietaIndex);
  }

  ngOnInit() {}
}
