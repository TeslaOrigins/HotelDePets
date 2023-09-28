import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TutorComponent } from './components/tutor/tutor.component';
import { CadastrarTutorComponent } from './components/tutor/cadastrar-tutor/cadastrar-tutor.component';
import { AlterarTutorComponent } from './components/tutor/alterar-tutor/alterar-tutor.component';
import { PetComponent } from './components/pet/pet.component';

const routes: Routes = [
  {
    path: 'tutor',
    component: TutorComponent,
    children: [
      {
        path: 'cadastrar',
        component: CadastrarTutorComponent,
      },
      {
        path: 'alterar',
        component: AlterarTutorComponent,
      },
    ],
  },
  {
    path: 'pet',
    component: PetComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
