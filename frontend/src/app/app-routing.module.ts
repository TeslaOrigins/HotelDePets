import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TutorComponent } from './components/tutor/tutor.component';
import { CadastrarTutorComponent } from './components/tutor/cadastrar-tutor/cadastrar-tutor.component';

const routes: Routes = [
  {
    path: 'tutor',
    component: TutorComponent,

  },
  {
    path: 'tutor/cadastrar',
    component: CadastrarTutorComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
