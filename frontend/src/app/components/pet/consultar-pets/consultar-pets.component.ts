import { Component } from '@angular/core';
import { Pet } from 'src/app/models/Pet';

@Component({
  selector: 'app-consultar-pets',
  templateUrl: './consultar-pets.component.html',
  styleUrls: ['./consultar-pets.component.scss'],
})
export class ConsultarPetsComponent {
  mockPets: Pet[] = [
    {
      petId: 1,
      nome: 'juba',
      idadeMes: 3,
      raca: 'nobre',
      sexo: 'Masculino',
      fotoUrl:
        'https://img.freepik.com/free-photo/puppy-that-is-walking-snow_1340-37228.jpg',
      peso: 30,
      especie: 'nobreza',
    },
    {
      petId: 2,
      nome: 'jujuba',
      idadeMes: 6,
      raca: 'nobre',
      sexo: 'Feminino',
      fotoUrl:
        'https://img.freepik.com/free-photo/puppy-that-is-walking-snow_1340-37228.jpg',
      peso: 60,
      especie: 'nobreza',
    },
    {
      petId: 2,
      nome: 'raimundo',
      idadeMes: 12,
      raca: 'ruim',
      sexo: 'Masculino',
      fotoUrl:
        'https://images.unsplash.com/photo-1598875706250-21faaf804361?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8NHx8fGVufDB8fHx8fA%3D%3D&w=1000&q=80',
      peso: 10,
      especie: 'vira-lata',
    },
  ];
  displayedColumns: string[] = [
    'fotoUrl',
    'nome',
    'idadeMes',
    'peso',
    'sexo',
    'raca',
    'especie',
  ];
}
