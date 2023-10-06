import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Pet } from 'src/app/models/Pet'; // Certifique-se de importar o modelo de Pet correto

@Component({
  selector: 'app-detalhes-pet',
  templateUrl: './detalhes-pet.component.html',
  styleUrls: ['./detalhes-pet.component.scss']
})
export class DetalhesPetComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: Pet) { }

  ngOnInit() {
  }

}