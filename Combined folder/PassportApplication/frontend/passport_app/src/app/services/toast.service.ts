import { Injectable } from "@angular/core";
import Swal from "sweetalert2";

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  showError(title: string) {
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'error',
      title: title,
      showConfirmButton: false,
      timer: 1500,
      timerProgressBar: true,
      background: "#f5f3f3",
      color: "#ba0e0e",
    });
  }

  showSuccess(title: string) {
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'success',
      title: title,
      showConfirmButton: false,
      timer: 1500,
      timerProgressBar: true,
      background: "#050505",
      color: "#2ce20c",
    });
  }

  showWarning(title: string) {
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'warning',
      title: title,
      showConfirmButton: false,
      timer: 1500,
      timerProgressBar: true,
      background: "#f5f3f3",
      color: "#e0a800",
    });
  }

  showInfo(title: string) {
    Swal.fire({
      toast: true,
      position: 'top-end',
      icon: 'info',
      title: title,
      showConfirmButton: false,
      timer: 1500,
      timerProgressBar: true,
      background: "#f5f3f3",
      color: "#17a2b8",
    });
  }
  
}
