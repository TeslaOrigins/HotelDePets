import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CpfValidator } from 'src/app/helpers/GenericValidator';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';
import { faPlus,faTrash } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';
@Component({
  selector: 'app-cadastrar-tutor',
  templateUrl: './cadastrar-tutor.component.html',
  styleUrls: ['./cadastrar-tutor.component.scss']
})
export class CadastrarTutorComponent implements OnInit {
  tutorForm: FormGroup;
  faPlus = faPlus;
  faTrash = faTrash;
  constructor(private builder: FormBuilder,
    private tutorService:TutorService,
    private toastr: ToastrService,
    private router: Router,
   public dialogRef: MatDialogRef<CadastrarTutorComponent>) {
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
    console.log(this.tutorForm);
    if (this.tutorForm.valid) {
      const obj = {
        nome: this.tutorForm.controls['nome'].value,
        telefone: this.tutorForm.controls['telefone'].value,
        email: this.tutorForm.controls['email'].value,
        cpf: this.tutorForm.controls['cpf'].value,
        enderecos: this.enderecos.value
      }
      const obs = {
        next: (tutor: Tutor) => {
          this.toastr.success('Tutor cadastrado com sucesso');
          this.router.navigateByUrl('/tutor/');
          this.dialogRef.close();
        },
        error: (err: any) => {
          if (err.status == 400) {
            err.error.forEach((element: string) => {
              this.toastr.error(element);
            });
          } else {
            this.toastr.error(err.error);
          }
          this.dialogRef.close();
        },
      };
      this.tutorService.cadastrar(obj).subscribe(obs);
    }
  }
}
