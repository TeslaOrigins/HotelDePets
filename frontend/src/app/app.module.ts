import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MatToolbarModule } from '@angular/material/toolbar';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
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
import { EditarTutorComponent } from './components/tutor/editar-tutor/editar-tutor.component';
import { DetalhesTutorComponent } from './components/tutor/detalhes-tutor/detalhes-tutor.component';
import { DialogConfirmacaoComponent } from './components/dialog-confirmacao/dialog-confirmacao.component';
import {
  ConsultarPetsComponent,
  DialogPet,
} from './components/pet/consultar-pet/consultar-pets.component';
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
    CadastrarHospedagemComponent,
    EditarHospedagemComponent,
    DetalhesHospedagemComponent,
    TutorComponent,
    CadastrarTutorComponent,
    EditarTutorComponent,
    DetalhesTutorComponent,
    DialogConfirmacaoComponent,
    ConsultarPetsComponent,
    DialogPet,
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
