import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  // tslint:disable-next-line: max-line-length
  // template : '<h1>{{title}}</h1>', //Ao invez de utilizar uma o html do arquivo (app.component.html), colocando o template: é possível escrever o html aqui (dentro de '') // comentario de cima "tslint...." desabilita a marcação para quando ultrapassar o núemros de caracteres digitados.
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ProAgil Eventos';
}
