import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';
import { EventoService } from "../_services/evento.service";
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
  // , providers: [EventoService] //permite injetar o serviço EventoService nesta classe
})
export class EventosComponent implements OnInit {

  titulo = 'Eventos';

  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  salvarNovo = true;
  bodyDeletarEvento: string;
  imagemLargura = 50;
  imagemAltura = 2;
  mostrarImagem = false;
  // modalRef: BsModalRef;
  registerForm: FormGroup;
  dataEvento: string;

  _filtroLista: string;

  constructor(
    private eventoService: EventoService
    , private modalService: BsModalService
    , private fb: FormBuilder
    , private localeService: BsLocaleService
    , private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  ngOnInit() {
    this.getEventos();
    this.validation();
  }

  novoEvento(template: any) {
    this.salvarNovo = true;
    this.openModal(template);
  }

  editarEvento(template: any, evento: Evento) {
    this.salvarNovo = false;
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  excluirEvento(template: any, evento: Evento) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
          this.toastr.success('Deletado com sucesso!');
        }, error => {
          this.toastr.error(`Erro ao tentar deletar: ${error}`);
          console.log(error);
        }
    );
  }

  openModal(template: any) {
    this.registerForm.reset();
    // this.modalRef = this.modalService.show(template);
    template.show();
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.salvarNovo) {
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            console.log(novoEvento);
            template.hide();
            this.getEventos();
            this.toastr.success('Adicionado com sucesso!');
          },
          error => {
            this.toastr.error(`Erro ao tentar inserir: ${error}`);
            console.log(error);
          }
        );
      }
      else {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);
        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
            this.toastr.success('Editado com sucesso!');
          },
          error => {
            this.toastr.error(`Erro ao tentar editar: ${error}`);
            console.log(error);
          }
        );
      }
    }
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      local: ['', Validators.required],
      dataEvento: ['', [Validators.required]],
      qtdPessoas: ['', [Validators.required, Validators.max(500),
        Validators.min(10)/*, Validators.pattern('^[0-9]*$')*/]], //validação numérica
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  filtrarEvento(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
        || evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
        || evento.dataEvento.toLocaleString().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventos.forEach(element => {
          var dataHoraAuxiliar = element.dataEvento.toString();
          var dataHoraSplit = dataHoraAuxiliar.split(" ");
          var dataAuxiliar = dataHoraSplit[0];
          var horaAuxiliar = dataHoraSplit[1];
          var dataSplit = dataAuxiliar.split("/");
          element.dataEvento = new Date(`${dataSplit[2]}/${dataSplit[1]}/${dataSplit[0]} ${horaAuxiliar}`);
        });
        this.eventosFiltrados = this.eventos;
        console.log(_eventos);
      },
      error => {
        this.toastr.error(`Erro ao tentar carregar: ${error}`);
        console.log(error);
      }
    );
  }
}
