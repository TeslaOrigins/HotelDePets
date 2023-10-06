import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MatToolbarModule } from '@angular/material/toolbar';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NavbarComponent } from './components/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MaterialModule } from './material.module';
import { HospedagemComponent } from './components/hospedagem/hospedagem.component';
import { CadastrarHospedagemComponent } from './components/hospedagem/cadastrar-hospedagem/cadastrar-hospedagem.component';
import { EditarHospedagemComponent } from './components/hospedagem/editar-hospedagem/editar-hospedagem.component';
import { DetalhesHospedagemComponent } from './components/hospedagem/detalhes-hospedagem/detalhes-hospedagem.component';
import { TutorComponent } from './components/tutor/tutor.component';
import { CadastrarTutorComponent } from './components/tutor/cadastrar-tutor/cadastrar-tutor.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AlterarTutorComponent } from './components/tutor/alterar-tutor/alterar-tutor.component';
import { DetalhesTutorComponent } from './components/tutor/detalhes-tutor/detalhes-tutor.component';
import { DialogConfirmacaoComponent } from './components/dialog-confirmacao/dialog-confirmacao.component';
import { PetComponent } from './components/pet/pet.component';
import { CadastrarPetComponent } from './components/pet/cadastrar-pet/cadastrar-pet.component';

import { DetalhesPetComponent } from './components/pet/detalhes-pet/detalhes-pet.component';
import { AlterarPetComponent } from './components/pet/alterar-pet/alterar-pet.component';
import { AlimentoComponent } from './components/alimento/alimento.component';
import { CadastrarAlimentoComponent } from './components/alimento/cadastrar-alimento/cadastrar-alimento.component';
import { AlterarAlimentoComponent } from './components/alimento/alterar-alimento/alterar-alimento.component';
import { DetalhesAlimentoComponent } from './components/alimento/detalhes-alimento/detalhes-alimento.component';
import {
  ConsultarServicosComponent,
  DialogServico,
} from './components/servico/consultar-servicos/consultar-servicos.component';
import {
  DialogMedicamento,
  ListarMedicamentosComponent,
} from './components/medicamento/listar-medicamentos/listar-medicamentos.component';
import {
  ConsultarReservaComponent,
  DialogReserva,
} from './components/reserva/consultar-reserva/consultar-reserva.component';

@NgModule({
  declarations: [
    AppComponent,
    HospedagemComponent,
    NavbarComponent,
    HomeComponent,
    CadastrarHospedagemComponent,
    EditarHospedagemComponent,
    DetalhesHospedagemComponent,
    TutorComponent,
    CadastrarTutorComponent,
    AlterarTutorComponent,
    DetalhesTutorComponent,
    DialogConfirmacaoComponent,
    PetComponent,
    CadastrarPetComponent,
    AlterarPetComponent,
    DetalhesPetComponent,
    AlimentoComponent,
    CadastrarAlimentoComponent,
    AlterarAlimentoComponent,
    DetalhesAlimentoComponent,
    ConsultarServicosComponent,
    DialogServico,
    DialogMedicamento,
    ListarMedicamentosComponent,
    ConsultarReservaComponent,
    DialogReserva,
  ],
  imports: [
    BrowserModule,
    FontAwesomeModule,
    AppRoutingModule,
    MatToolbarModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    ToastrModule.forRoot(),
    FontAwesomeModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
