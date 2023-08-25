import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-cadastrar-tutor',
  templateUrl: './cadastrar-tutor.component.html',
  styleUrls: ['./cadastrar-tutor.component.css']
})
export class CadastrarTutorComponent implements OnInit {
  tutorForm: FormGroup;
  constructor(builder: FormBuilder) {
    this.tutorForm = builder.group({
      name: new FormControl<string>('',Validators.required),
      
    });
  }
  ngOnInit() {
  }

}
