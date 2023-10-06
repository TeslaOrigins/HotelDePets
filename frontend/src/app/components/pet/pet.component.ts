import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Pet } from 'src/app/models/Pet'; // Certifique-se de importar seu modelo Pet
import { PetService } from 'src/app/services/pet.service'; // Certifique-se de importar seu serviço PetService
import { faMagnifyingGlass, faTrash, faEdit, faPlus } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { CadastrarPetComponent } from './cadastrar-pet/cadastrar-pet.component'; // Certifique-se de importar seu componente de cadastro de pet
import { EditarPetComponent } from './editar-pet/editar-pet.component'; // Certifique-se de importar seu componente de edição de pet
import { DialogConfirmacaoComponent } from '../dialog-confirmacao/dialog-confirmacao.component';
import { DetalhesPetComponent } from './detalhes-pet/detalhes-pet.component'; // Certifique-se de importar seu componente de detalhes de pet

@Component({
  selector: 'app-pet',
  templateUrl: './pet.component.html',
  styleUrls: ['./pet.component.scss']
})
export class PetComponent implements OnInit {
  pets: Pet[] = [];
  faMagnfyingGlass = faMagnifyingGlass;
  faTrash = faTrash;
  faEdit = faEdit;
  faPlus = faPlus;

  constructor(
    private petService: PetService, // Certifique-se de importar seu serviço PetService
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.petService.getAllPets().subscribe((response: any) => {
      if (response && response.data) {
        this.pets = response.data;
      } else {
        this.pets = [];
      }
    });
  }
  
  openDialogDetalhes(pet: Pet): void {
    const dialogRef = this.dialog.open(DetalhesPetComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: pet
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(CadastrarPetComponent, {
      autoFocus: false,
      maxHeight: '90vh',
      width: '1250px'
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      // Lógica após fechar o diálogo de cadastro
    });
  }

  openDialogEdit(pet: Pet): void {
    const dialogRef = this.dialog.open(EditarPetComponent, {
      width: '1250px',
      autoFocus: false,
      maxHeight: '90vh',
      data: pet
    });
  }
  
  deletarPet(pet: Pet) {
    const dialogRef = this.dialog.open(DialogConfirmacaoComponent, {
      data: 'Tem certeza que quer deletar o pet com o nome: ' + pet.nome + ' ?'
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        const obs = {
          next: (msg: string) => {
            this.toastr.success(msg);
          },
          error: (err: any) => {
            err.forEach((error: any) => {
              this.toastr.error(error);
            });
          },
        };
        this.petService.deletar(pet.id).subscribe(obs);
      }
    });
  }
}