import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'feedbackComplaintType',
  standalone:true
})
export class FeedbackComplaintTypePipe implements PipeTransform {

  transform(value: number): string {
    switch (value) {
      case 1:
        return 'Feedback';
      case 2:
        return 'Complaint';
      default:
        return 'Unknown';
    }
  }

}
