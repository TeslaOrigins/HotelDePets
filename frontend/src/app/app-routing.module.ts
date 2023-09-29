import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TutorComponent } from './components/tutor/tutor.component';
import { CadastrarTutorComponent } from './components/tutor/cadastrar-tutor/cadastrar-tutor.component';
import { AlterarTutorComponent } from './components/tutor/alterar-tutor/alterar-tutor.component';
import { PetComponent } from './components/pet/pet.component';
import { AlimentoComponent } from './components/alimento/alimento.component';

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
  {
    path: 'alimento',
    component: AlimentoComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
