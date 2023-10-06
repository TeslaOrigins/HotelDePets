import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CpfValidator } from 'src/app/helpers/GenericValidator';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';
import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';
import { EnderecoService } from 'src/app/services/endereco.service';
import { Endereco } from 'src/app/models/Endereco';

@Component({
  selector: 'app-cadastrar-tutor',
  templateUrl: './cadastrar-tutor.component.html',
  styleUrls: ['./cadastrar-tutor.component.scss']
})
export class CadastrarTutorComponent implements OnInit {
  tutorForm: FormGroup;
  faPlus = faPlus;
  faTrash = faTrash;

  constructor(
    private builder: FormBuilder,
    private enderecoService: EnderecoService,
    private tutorService: TutorService,
    private toastr: ToastrService,
    private router: Router,
    public dialogRef: MatDialogRef<CadastrarTutorComponent>
  ) {
    this.tutorForm = this.builder.group({
      nome: new FormControl<string>('', Validators.required),
      nome_normalizado: new FormControl<string>('', Validators.required),
      telefone: new FormControl<string>('', [Validators.required, Validators.minLength(11)]),
      email: new FormControl<string>('', [Validators.email, Validators.required]),
      cpf: new FormControl<string>('', [Validators.required, CpfValidator()]),
      enderecos: this.builder.array([
        this.builder.group({
          logradouro: new FormControl<string>('', Validators.required),
          numero: new FormControl<string>('', Validators.required),
          cidade: new FormControl<string>('', Validators.required),
          estado: new FormControl<string>('', Validators.required)
        })
      ], Validators.required)
    });
  }

  ngOnInit() {
  }

  get enderecos() {
    return this.tutorForm.controls['enderecos'] as FormArray;
  }

  pushNovoEndereco() {
    const enderecoForm = this.builder.group({
      logradouro: new FormControl<string>('', Validators.required),
      numero: new FormControl<string>('', Validators.required),
      cidade: new FormControl<string>('', Validators.required),
      estado: new FormControl<string>('', Validators.required)
    });
    this.enderecos.push(enderecoForm);
  }

  deleteEndereco(enderecoIndex: number) {
    this.enderecos.removeAt(enderecoIndex);
  }

  cadastrar() {
    if (this.tutorForm.valid) {
      const obj = {
        nome: this.tutorForm.controls['nome'].value,
        nome_normalizado: this.tutorForm.controls['nome_normalizado'].value,
        telefone: this.tutorForm.controls['telefone'].value,
        email: this.tutorForm.controls['email'].value,
        cpf: this.tutorForm.controls['cpf'].value,
      };

      this.tutorService.cadastrar(obj).subscribe(
        (response: any) => {
          if (response && response.data) {
            const tutorId = response.data.id; 

            const enderecos = this.enderecos.value.map((endereco: any) => ({
              ...endereco,
              tutor_id: tutorId, 
            }));

            enderecos.forEach((endereco: any) => {
              this.enderecoService.cadastrarEndereco(endereco).subscribe(
                (enderecoCadastrado: Endereco) => {
                  console.log('Endereço cadastrado:', enderecoCadastrado);
                },
                (error: any) => {
                  console.error('Erro ao cadastrar endereço:', error);
                }
              );
            });

            this.toastr.success('Tutor cadastrado com sucesso');
            this.router.navigateByUrl('/tutores/');
            this.dialogRef.close();
          }
        },
        (error: any) => {
          console.error('Erro ao cadastrar tutor:', error);
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