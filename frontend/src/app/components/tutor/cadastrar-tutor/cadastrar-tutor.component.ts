import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CpfValidator } from 'src/app/helpers/GenericValidator';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';

@Component({
  selector: 'app-cadastrar-tutor',
  templateUrl: './cadastrar-tutor.component.html',
  styleUrls: ['./cadastrar-tutor.component.css']
})
export class CadastrarTutorComponent implements OnInit {
  tutorForm: FormGroup;
  constructor(private builder: FormBuilder,
    private tutorService:TutorService,
    private toastr: ToastrService,
    private router: Router) {
    this.tutorForm = this.builder.group({
      name: new FormControl<string>('',Validators.required),
      telefone: new FormControl<string>('',[Validators.required,Validators.minLength(11)]),
      email: new FormControl<string>('',[Validators.email,Validators.required]),
      cpf: new FormControl<string>('',[Validators.required,CpfValidator()])
    });
  }
  ngOnInit() {
  }
  cadastrar(){
    if (this.tutorForm.valid) {
      const obj = {
        name: this.tutorForm.controls['name'].value,
        telefone: this.tutorForm.controls['name'].value,
        email: this.tutorForm.controls['name'].value,
        cpf: this.tutorForm.controls['name'].value,

      };
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
