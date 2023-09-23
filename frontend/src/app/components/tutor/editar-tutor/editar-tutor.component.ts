import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { CpfValidator } from 'src/app/helpers/GenericValidator';
import { Endereco } from 'src/app/models/Endereco';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';

@Component({
  selector: 'app-editar-tutor',
  templateUrl: './editar-tutor.component.html',
  styleUrls: ['./editar-tutor.component.scss']
})
export class EditarTutorComponent implements OnInit {

  tutorForm!: FormGroup;
  faPlus = faPlus;
  faTrash = faTrash;
  tutorInalterado!: Tutor;
  tutor$: Observable<Tutor>;
  constructor(private builder: FormBuilder,
    private tutorService:TutorService,
    private toastr: ToastrService,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: Tutor,
    public dialogRef: MatDialogRef<EditarTutorComponent>) {

    this.tutor$ = this.tutorService.getTutorById(data.tutorId);
    this.tutor$.subscribe((tutor:Tutor )=> {
      this.tutorInalterado =tutor;
      this.tutorForm = this.builder.group({
        nome: new FormControl<string>(this.tutorInalterado.nome,Validators.required),
        telefone: new FormControl<string>(this.tutorInalterado.telefone,[Validators.required,Validators.minLength(11)]),
        email: new FormControl<string>(this.tutorInalterado.email,[Validators.email,Validators.required]),
        cpf: new FormControl<string>(this.tutorInalterado.cpf,[Validators.required,CpfValidator()]),
        enderecos:this.builder.array([

        ],Validators.required)
      });
      this.tutorInalterado.enderecos.forEach(e => this.pushNovoEnderecoExistente(e));
    })




  }
  ngOnInit() {
  }
  get enderecos() {
    return this.tutorForm.controls['enderecos'] as FormArray;
  }

  pushNovoEndereco(){
    const enderecoForm = this.builder.group({
      enderecoId: new FormControl<number>(0),
      logradouro: new FormControl<string>('',Validators.required),
      numero :  new FormControl<string>('',Validators.required),
      cidade :  new FormControl<string>('',Validators.required),
      estado :  new FormControl<string>('',Validators.required)
    });
    this.enderecos.push(enderecoForm);
  }
  pushNovoEnderecoExistente(endereco: Endereco){
    const enderecoForm = this.builder.group({
      enderecoId: new FormControl<number>(endereco.enderecoId),
      logradouro: new FormControl<string>(endereco.logradouro,Validators.required),
      numero :  new FormControl<string>(endereco.numero,Validators.required),
      cidade :  new FormControl<string>(endereco.cidade,Validators.required),
      estado :  new FormControl<string>(endereco.estado,Validators.required)
    });
    this.enderecos.push(enderecoForm);
  }


  deleteEndereco(enderecoIndex: number) {
    this.enderecos.removeAt(enderecoIndex);
  }
  editar(){
    if (this.tutorForm.valid) {
      const obj = {
        tutorId: this.tutorInalterado.tutorId,
        nome: this.tutorForm.controls['nome'].value,
        telefone: this.tutorForm.controls['telefone'].value,
        email: this.tutorForm.controls['email'].value,
        cpf: this.tutorForm.controls['cpf'].value,
        enderecos: this.enderecos.value
      }
      const obs = {
        next: (tutor: Tutor) => {
          this.toastr.success('Tutor editado com sucesso');
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
      this.tutorService.editar(obj).subscribe(obs);
    }
  }
}
