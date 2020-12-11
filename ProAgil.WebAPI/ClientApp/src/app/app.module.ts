import { AppComponent } from './app.component';

import { EventosComponent } from './eventos/eventos.component'; //utilizar desta forma quando for importar aquivo que está no mesmo diretorio
import { NavComponent } from './nav/nav.component';

import { DateTimeFormatPipePipe } from "./_helps/DateTimeFormatPipe.pipe";

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '../../node_modules/@angular/common/http'; //importar desta forma, ou direto com @, igual os import de cima
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgxMaskModule } from 'ngx-mask';

// import { EventoService } from './_services/evento.service';

@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    NavComponent,
    DateTimeFormatPipePipe
  ],
  imports: [
    BrowserModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    NgxMaskModule.forRoot()
  ],
  providers: [
    //EventoService //permite injetar o serviço EventoService em qualquer classe do projeto. Pode ser deixado ele aqui desta maniera, ou na propria classe Service (EventoService), dentro do Injectable.
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
