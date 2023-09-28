import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { CpfValidator } from 'src/app/helpers/GenericValidator';
import { Alimento } from 'src/app/models/Alimento';
import { Tutor } from 'src/app/models/Tutor';
import { AlimentoService } from 'src/app/services/alimento.service';

@Component({
  selector: 'app-cadastrar-alimento',
  templateUrl: './cadastrar-alimento.component.html',
  styleUrls: ['./cadastrar-alimento.component.css']
})
export class CadastrarAlimentoComponent implements OnInit {
  alimentoForm: FormGroup;
  faPlus = faPlus;
  faTrash = faTrash;
  constructor(private builder: FormBuilder,
    private alimentoService:AlimentoService,
    private toastr: ToastrService,
    private router: Router,
   public dialogRef: MatDialogRef<CadastrarAlimentoComponent>) {
    this.alimentoForm = this.builder.group({
      nome: new FormControl<string>('',Validators.required),
      quantidadeEstoque: new FormControl<number>(0,[Validators.required,Validators.minLength(11)]),
      precoReabastecimento: new FormControl<number>(0,Validators.required),
      dataEntrada: new FormControl<Date>(new Date(),Validators.required),

    });

  }
  ngOnInit() {
  }

  cadastrar(){
    console.log(this.alimentoForm);
    if (this.alimentoForm.valid) {
      const obj = {
        nome: this.alimentoForm.controls['nome'].value,
        quantidadeEstoque: this.alimentoForm.controls['quantidadeEstoque'].value,
        precoReabastecimento: this.alimentoForm.controls['precoReabastecimento'].value,
        dataEntrada: this.alimentoForm.controls['dataEntrada'].value,

      }
      const obs = {
        next: (alimento: Alimento) => {
          this.toastr.success('Alimento cadastrado com sucesso');
          this.router.navigateByUrl('/alimento/');
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
      this.alimentoService.cadastrar(obj).subscribe(obs);
    }
  }
}
