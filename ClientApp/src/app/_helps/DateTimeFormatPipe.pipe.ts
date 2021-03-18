import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { DateFormatter } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { Constants } from "../util/Constants";

@Pipe({
  name: 'DateTimeFormatPipe'
})
export class DateTimeFormatPipePipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value, Constants.DATE_TIME_FMT);
  }

}

// export class DateTimeFormatPipePipe implements PipeTransform {

//   transform(value: any, args?: any): any {
//     var datePipe = new Date(value);
//     return moment(datePipe).format('dd/MM/yyyy HH:mm');
//   }

// }
