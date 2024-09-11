import { Component, inject, NgModule, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet, ActivatedRoute } from '@angular/router';
import { SectionComponent } from './section/section.component';
import { FormsModule, NgModel } from '@angular/forms';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-application-form',
  standalone: true,
  imports: [RouterOutlet, RouterLink , NgClass],
  templateUrl: './application-form.component.html',
  styleUrls: ['./application-form.component.css']
})
export class ApplicationFormComponent {

}
