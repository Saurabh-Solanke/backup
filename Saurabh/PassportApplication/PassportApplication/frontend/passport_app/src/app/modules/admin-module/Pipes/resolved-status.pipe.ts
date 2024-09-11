import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'resolvedStatus',
  standalone:true
})
export class ResolvedStatusPipe implements PipeTransform {

  transform(value: number): string {
    switch (value) {
      case 1:
        return 'Resolved';
      case 2:
        return 'Unresolved';
      default:
        return 'Unknown';
    }
  }

}
