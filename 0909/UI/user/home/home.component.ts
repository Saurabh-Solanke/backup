import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  name: string | null;

  constructor() {
    this.name = sessionStorage.getItem('name');
  }
}
