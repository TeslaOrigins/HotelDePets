import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CpfValidator } from 'src/app/helpers/GenericValidator';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-editar-tutor',
  templateUrl: './editar-tutor.component.html',
  styleUrls: ['./editar-tutor.component.scss']
})
export class EditarTutorComponent implements OnInit {
  tutorForm: FormGroup;
  faPlus = faPlus;
  tutorId: number;

  constructor(
    private builder: FormBuilder,
    private tutorService: TutorService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute,
    public dialogRef: MatDialogRef<EditarTutorComponent>
  ) {
    this.tutorId = 0;
    this.tutorForm = this.builder.group({
      tutorId: [0], // Inclua o tutorId aqui com um valor inicial de 0
      nome: new FormControl<string>('', Validators.required),
      nome_normalizado: new FormControl<string>('', Validators.required),
      telefone: new FormControl<string>('', [Validators.required, Validators.minLength(11)]),
      email: new FormControl<string>('', [Validators.email, Validators.required]),
      cpf: new FormControl<string>('', [Validators.required, CpfValidator()])
    });
  }

  ngOnInit() {
    // Obtenha o ID do tutor da rota
    this.route.params.subscribe(params => {
      const id = +params['id']; // Certifique-se de converter para número
      if (!isNaN(id)) {
        this.tutorId = id;
        // Agora você pode fazer a chamada para o serviço com this.tutorId
        this.tutorService.getTutorById(this.tutorId).subscribe((tutor: Tutor) => {
          // ...
        });
      } else {
        // Lide com o cenário em que o ID não é um número válido
        console.error('ID inválido:', params['id']);
      }
    });
  }

  editar() {
    if (this.tutorForm.valid) {
      const obj = {
        tutorId: this.tutorForm.controls['tutorId'].value,
        nome: this.tutorForm.controls['nome'].value,
        nome_normalizado: this.tutorForm.controls['nome_normalizado'].value,
        telefone: this.tutorForm.controls['telefone'].value,
        email: this.tutorForm.controls['email'].value,
        cpf: this.tutorForm.controls['cpf'].value,
      };

      // Chama o método de edição do TutorService
      this.tutorService.editar(obj.tutorId, obj).subscribe(
        (tutorEditado: Tutor) => {
          // Tutor editado com sucesso
          console.log('Tutor editado:', tutorEditado);
          
          // Continue o tratamento de sucesso do tutor
          this.toastr.success('Tutor editado com sucesso');
          this.router.navigateByUrl(`/tutores/detalhes/${tutorEditado.id}`);
          this.dialogRef.close();
        },
        (error: any) => {
          console.error('Erro ao editar tutor:', error);
          // Trate os erros relacionados à edição do tutor aqui
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