import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { CpfValidator } from 'src/app/helpers/GenericValidator';
import { Alimento } from 'src/app/models/Alimento';
import { AlimentoService } from 'src/app/services/alimento.service';
import { CadastrarAlimentoComponent } from '../cadastrar-alimento/cadastrar-alimento.component';
import { Tutor } from 'src/app/models/Tutor';

@Component({
  selector: 'app-alterar-alimento',
  templateUrl: './alterar-alimento.component.html',
  styleUrls: ['./alterar-alimento.component.css'],
})
export class AlterarAlimentoComponent implements OnInit {
  alimentoForm: FormGroup;
  faPlus = faPlus;
  faTrash = faTrash;
  constructor(private builder: FormBuilder,
    private alimentoService:AlimentoService,
    private toastr: ToastrService,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: Alimento,
    public dialogRef: MatDialogRef<AlterarAlimentoComponent>) {
    this.alimentoForm = this.builder.group({
      nome: new FormControl<string>(data.nome,Validators.required),
      quantidadeEstoque: new FormControl<number>(data.quantidadeEstoque,[Validators.required,Validators.minLength(11)]),
      precoReabastecimento: new FormControl<number>(data.precoReabastecimento,Validators.required),
      dataEntrada: new FormControl<Date>(data.dataEntrada,Validators.required),

    });

  }
  ngOnInit() {
  }

  cadastrar(){
    console.log(this.alimentoForm);
    if (this.alimentoForm.valid) {
      const obj = {
        alimentoId: this.data.alimentoId,
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
