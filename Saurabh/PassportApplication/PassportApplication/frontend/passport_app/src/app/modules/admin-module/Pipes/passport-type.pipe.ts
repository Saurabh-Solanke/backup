import { Pipe, PipeTransform } from '@angular/core';

export enum PassportType {
  New = 1,
  Renewal = 2,
}

@Pipe({
  name: 'passportType',
  standalone: true,
})
export class PassportTypePipe implements PipeTransform {
  transform(value: PassportType): string {
    switch (value) {
      case PassportType.New:
        return 'New';
      case PassportType.Renewal:
        return 'Renewal';
      default:
        return 'Unknown';
    }
  }
}
