import { Component,Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Tutor } from 'src/app/models/Tutor';

@Component({
  selector: 'app-detalhes-tutor',
  templateUrl: './detalhes-tutor.component.html',
  styleUrls: ['./detalhes-tutor.component.scss']
})
export class DetalhesTutorComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: Tutor) { }

  ngOnInit() {
  }

}
