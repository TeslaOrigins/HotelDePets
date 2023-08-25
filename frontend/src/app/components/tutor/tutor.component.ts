import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Tutor } from 'src/app/models/Tutor';
import { TutorService } from 'src/app/services/tutor.service';

@Component({
  selector: 'app-tutor',
  templateUrl: './tutor.component.html',
  styleUrls: ['./tutor.component.css']
})
export class TutorComponent implements OnInit {
  tutores: Tutor[] =[];
  tutores$ : Observable<Tutor[]>;
  constructor(tutorService: TutorService,
              toastr: ToastrService) {
    this.tutores$ = tutorService.getAllTutores();
    const obs = {
      next: (tutores: Tutor[]) => {
        this.tutores = tutores;
      },
      error: (err: any) => {
        err.forEach((error: any) => {
          toastr.error(error);
        });
      },
    };
    this.tutores$.subscribe(obs);
  }

  ngOnInit() {
  }

}
