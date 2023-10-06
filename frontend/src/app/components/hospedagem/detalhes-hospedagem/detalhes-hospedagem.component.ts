import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Hospedagem } from 'src/app/models/Hospedagem';

@Component({
  selector: 'app-detalhes-hospedagem',
  templateUrl: './detalhes-hospedagem.component.html',
  styleUrls: ['./detalhes-hospedagem.component.scss']
})
export class DetalhesHospedagemComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: Hospedagem) { }

  ngOnInit() {
  }

}