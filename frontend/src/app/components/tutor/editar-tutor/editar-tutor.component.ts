import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { CpfValidator } from 'src/app/helpers/GenericValidator';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';

@Component({
  selector: 'app-editar-tutor',
  templateUrl: './editar-tutor.component.html',
  styleUrls: ['./editar-tutor.component.scss']
})
export class EditarTutorComponent implements OnInit {

  tutorForm: FormGroup;
  faPlus = faPlus;
  faTrash = faTrash;
  constructor(private builder: FormBuilder,
    private tutorService:TutorService,
    private toastr: ToastrService,
    private router: Router) {
    this.tutorForm = this.builder.group({
      nome: new FormControl<string>('',Validators.required),
      telefone: new FormControl<string>('',[Validators.required,Validators.minLength(11)]),
      email: new FormControl<string>('',[Validators.email,Validators.required]),
      cpf: new FormControl<string>('',[Validators.required,CpfValidator()]),
      enderecos:this.builder.array([
        this.builder.group({
          logradouro: new FormControl<string>('',Validators.required),
          numero :  new FormControl<string>('',Validators.required),
          cidade :  new FormControl<string>('',Validators.required),
          estado :  new FormControl<string>('',Validators.required)
        })
      ],Validators.required)
    });

  }
  ngOnInit() {
  }
  get enderecos() {
    return this.tutorForm.controls['enderecos'] as FormArray;
  }

  pushNovoEndereco(){
    const enderecoForm = this.builder.group({
      logradouro: new FormControl<string>('',Validators.required),
      numero :  new FormControl<string>('',Validators.required),
      cidade :  new FormControl<string>('',Validators.required),
      estado :  new FormControl<string>('',Validators.required)
    });
    this.enderecos.push(enderecoForm);
  }


  deleteEndereco(enderecoIndex: number) {
    this.enderecos.removeAt(enderecoIndex);
  }
  cadastrar(){
    if (this.tutorForm.valid) {
      const obj = {
        nome: this.tutorForm.controls['nome'].value,
        telefone: this.tutorForm.controls['telefone'].value,
        cidade: this.tutorForm.controls['cidade'].value,
        estado: this.tutorForm.controls['estado'].value,
        enderecos: this.enderecos
      }
      const obs = {
        next: (tutor: Tutor) => {
          this.toastr.success('Tutor cadastrado com sucesso');
          this.router.navigateByUrl('/tutor/');
        },
        error: (err: any) => {
          if (err.status == 400) {
            err.error.forEach((element: string) => {
              this.toastr.error(element);
            });
          } else {
            this.toastr.error(err.error);
          }
        },
      };
      this.tutorService.cadastrar(obj).subscribe(obs);
    }
  }
}
