import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Hospedagem } from 'src/app/models/Hospedagem'; // Certifique-se de importar seu modelo Hospedagem
import { HospedagemService } from 'src/app/services/hospedagem.service'; // Certifique-se de importar seu serviço HospedagemService

@Component({
  selector: 'app-editar-hospedagem',
  templateUrl: './editar-hospedagem.component.html',
  styleUrls: ['./editar-hospedagem.component.scss']
})
export class EditarHospedagemComponent implements OnInit {

  hospedagemForm!: FormGroup;
  faPlus = faPlus;
  faTrash = faTrash;
  hospedagemInalterada!: Hospedagem; // Certifique-se de importar seu modelo Hospedagem
  hospedagem$: Observable<Hospedagem>; // Certifique-se de importar seu modelo Hospedagem
  constructor(private builder: FormBuilder,
    private hospedagemService: HospedagemService, // Certifique-se de importar seu serviço HospedagemService
    private toastr: ToastrService,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: Hospedagem, // Certifique-se de importar seu modelo Hospedagem
    public dialogRef: MatDialogRef<EditarHospedagemComponent>) {

    this.hospedagem$ = this.hospedagemService.getHospedagemById(data.hospedagemId); // Certifique-se de importar seu serviço HospedagemService
    this.hospedagem$.subscribe((hospedagem: Hospedagem) => { // Certifique-se de importar seu modelo Hospedagem
      this.hospedagemInalterada = hospedagem;
      this.hospedagemForm = this.builder.group({
        dataEntrada: new FormControl<Date>(this.hospedagemInalterada.dataEntrada, Validators.required),
        dataSaida: new FormControl<Date>(this.hospedagemInalterada.dataSaida, Validators.required),
        observacoes: new FormControl<string>(this.hospedagemInalterada.observacoes),
        checkIn: new FormControl<boolean>(this.hospedagemInalterada.checkIn),
        petId: new FormControl<number>(this.hospedagemInalterada.petId, Validators.required),
        precoHospedagem: new FormControl<number>(this.hospedagemInalterada.precoHospedagem),
        // Adicione os campos adicionais aqui conforme necessário
      });
    });
  }

  ngOnInit() {
  }

  // Implemente a parte do formulário para a lista de endereços aqui, similar ao que você fez no formulário de tutor

  editar() {
    if (this.hospedagemForm.valid) {
      const obj = {
        hospedagemId: this.hospedagemInalterada.hospedagemId,
        dataEntrada: this.hospedagemForm.controls['dataEntrada'].value,
        dataSaida: this.hospedagemForm.controls['dataSaida'].value,
        observacoes: this.hospedagemForm.controls['observacoes'].value,
        checkIn: this.hospedagemForm.controls['checkIn'].value,
        petId: this.hospedagemForm.controls['petId'].value,
        precoHospedagem: this.hospedagemForm.controls['precoHospedagem'].value,
        // Adicione os campos adicionais aqui conforme necessário
      }
      const obs = {
        next: (hospedagem: Hospedagem) => { // Certifique-se de importar seu modelo Hospedagem
          this.toastr.success('Hospedagem editada com sucesso');
          this.router.navigateByUrl('/hospedagem/'); // Certifique-se de ajustar a rota correta
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
      this.hospedagemService.editar(obj).subscribe(obs); // Certifique-se de importar seu serviço HospedagemService
    }
  }
}