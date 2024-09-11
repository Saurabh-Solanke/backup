import { Component, OnInit } from '@angular/core';
import { RouterModule, NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        if (event.url === '/landing-page') {  
          window.history.replaceState({}, '', '/');
          window.location.reload();
        }
      }
    });
  }
}





