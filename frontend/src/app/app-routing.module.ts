import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TutorComponent } from './components/tutor/tutor.component';
import { CadastrarTutorComponent } from './components/tutor/cadastrar-tutor/cadastrar-tutor.component';
import { EditarTutorComponent } from './components/tutor/editar-tutor/editar-tutor.component';
import { ConsultarPetsComponent } from './components/pet/consultar-pet/consultar-pets.component';
import { ConsultarServicosComponent } from './components/servico/consultar-servicos/consultar-servicos.component';
import { ListarMedicamentosComponent } from './components/medicamento/listar-medicamentos/listar-medicamentos.component';
import { ConsultarReservaComponent } from './components/reserva/consultar-reserva/consultar-reserva.component';

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
    path: 'servico',
    component: ConsultarServicosComponent,
  },
  {
    path: 'medicamentos',
    component: ListarMedicamentosComponent,
  },
  {
    path: 'reserva',
    component: ConsultarReservaComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
