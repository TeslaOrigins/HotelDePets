import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HospedagemService } from 'src/app/services/hospedagem.service';
import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-cadastrar-hospedagem',
  templateUrl: './cadastrar-hospedagem.component.html',
  styleUrls: ['./cadastrar-hospedagem.component.scss']
})
export class CadastrarHospedagemComponent implements OnInit {
  hospedagemForm: FormGroup;
  faPlus = faPlus;
  faTrash = faTrash;

  constructor(
    private builder: FormBuilder,
    private hospedagemService: HospedagemService,
    private toastr: ToastrService,
    private router: Router,
    public dialogRef: MatDialogRef<CadastrarHospedagemComponent>
  ) {
    this.hospedagemForm = this.builder.group({
      dataEntrada: new FormControl<Date>(new Date(), Validators.required),
      dataSaida: new FormControl<Date>(new Date(), Validators.required),
      observacoes: new FormControl<string>(''),
      checkIn: new FormControl<boolean>(false),
      petId: new FormControl<number | null>(null),
      precoHospedagem: new FormControl<number | null>(null),
    });
  }

  ngOnInit() {}

  cadastrar() {
    if (this.hospedagemForm.valid) {
      const obj = {
        dataEntrada: this.hospedagemForm.controls['dataEntrada'].value,
        dataSaida: this.hospedagemForm.controls['dataSaida'].value,
        observacoes: this.hospedagemForm.controls['observacoes'].value,
        checkIn: this.hospedagemForm.controls['checkIn'].value,
        petId: this.hospedagemForm.controls['petId'].value,
        precoHospedagem: this.hospedagemForm.controls['precoHospedagem'].value,
      };

      const obs = {
        next: () => {
          this.toastr.success('Hospedagem cadastrada com sucesso');
          this.router.navigateByUrl('/hospedagem/');
          this.dialogRef.close();
        },
        error: (err: any) => {
          if (err.status === 400) {
            err.error.forEach((element: string) => {
              this.toastr.error(element);
            });
          } else {
            this.toastr.error(err.error);
          }
          this.dialogRef.close();
        },
      };

      this.hospedagemService.cadastrar(obj).subscribe(obs);
    }
  }
}