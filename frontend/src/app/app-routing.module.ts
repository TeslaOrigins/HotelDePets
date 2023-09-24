import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TutorComponent } from './components/tutor/tutor.component';
import { CadastrarTutorComponent } from './components/tutor/cadastrar-tutor/cadastrar-tutor.component';
import { EditarTutorComponent } from './components/tutor/editar-tutor/editar-tutor.component';
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
        path: 'editar',
        component: EditarTutorComponent,
      },
    ],
  },
  {
    path: 'pet',
    component: PetComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
