import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { TutorComponent } from './components/tutor/tutor.component';
import { CadastrarTutorComponent } from './components/tutor/cadastrar-tutor/cadastrar-tutor.component';
import { AlterarTutorComponent } from './components/tutor/alterar-tutor/alterar-tutor.component';
import { PetComponent } from './components/pet/pet.component';
import { AlimentoComponent } from './components/alimento/alimento.component';
import { HospedagemComponent } from './components/hospedagem/hospedagem.component';
import { CadastrarHospedagemComponent } from './components/hospedagem/cadastrar-hospedagem/cadastrar-hospedagem.component';
import { EditarHospedagemComponent } from './components/hospedagem/editar-hospedagem/editar-hospedagem.component';
import { ConsultarServicosComponent } from './components/servico/consultar-servicos/consultar-servicos.component';
import { ListarMedicamentosComponent } from './components/medicamento/listar-medicamentos/listar-medicamentos.component';
import { ConsultarReservaComponent } from './components/reserva/consultar-reserva/consultar-reserva.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
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
  {
    path: 'hospedagem',
    component: HospedagemComponent,
    children: [
      {
        path: 'cadastrar',
        component: CadastrarHospedagemComponent,
      },
      {
        path: 'editar',
        component: EditarHospedagemComponent,
      },
    ],
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
