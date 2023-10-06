import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Alimento } from 'src/app/models/Alimento';

@Component({
  selector: 'app-detalhes-alimento',
  templateUrl: './detalhes-alimento.component.html',
  styleUrls: ['./detalhes-alimento.component.css']
})
export class DetalhesAlimentoComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: Alimento) { }

  ngOnInit() {
  }
}
