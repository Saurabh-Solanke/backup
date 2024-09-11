import { Pipe, PipeTransform } from '@angular/core';

export enum ApplicationStatus {
  Pending = 1,
  Approved = 2,
  Rejected = 3,
}

@Pipe({
  name: 'applicationStatus',
  standalone: true,
})
export class ApplicationStatusPipe implements PipeTransform {
  transform(value: ApplicationStatus): string {
    switch (value) {
      case ApplicationStatus.Pending:
        return 'Pending';
      case ApplicationStatus.Approved:
        return 'Approved';
      case ApplicationStatus.Rejected:
        return 'Rejected';
      default:
        return 'Unknown';
    }
  }
}
