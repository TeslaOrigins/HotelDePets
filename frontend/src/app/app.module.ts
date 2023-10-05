import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
@NgModule({
  declarations: [
    AppComponent,
    HospedagemComponent,
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
  ],
  imports: [
    BrowserModule,
    FontAwesomeModule,
    AppRoutingModule,
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
