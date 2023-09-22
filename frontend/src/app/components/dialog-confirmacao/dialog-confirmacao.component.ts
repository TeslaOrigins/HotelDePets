import { Component,Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-confirmacao',
  templateUrl: './dialog-confirmacao.component.html',
  styleUrls: ['./dialog-confirmacao.component.scss']
})
export class DialogConfirmacaoComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DialogConfirmacaoComponent>,
              @Inject(MAT_DIALOG_DATA) public data: string) {}


  ngOnInit() {
  }

}
