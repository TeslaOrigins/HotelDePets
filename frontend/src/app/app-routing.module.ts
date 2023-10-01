import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TutorComponent } from './components/tutor/tutor.component';
import { CadastrarTutorComponent } from './components/tutor/cadastrar-tutor/cadastrar-tutor.component';
import { EditarTutorComponent } from './components/tutor/editar-tutor/editar-tutor.component';
import { ConsultarPetsComponent } from './components/pet/consultar-pet/consultar-pets.component';
import { ListarMedicamentosComponent } from './components/medicamento/listar-medicamentos/listar-medicamentos.component';

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
    path: 'pets',
    component: ConsultarPetsComponent,
  },
  {
    path: 'medicamentos',
    component: ListarMedicamentosComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
