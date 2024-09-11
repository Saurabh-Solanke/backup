import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { HomeComponent } from './home/home.component';
import { UsersDashboardComponent } from './users-dashboard/users-dashboard.component';
import { TestHistoryDashboardComponent } from './test-history-dashboard/test-history-dashboard.component';
import { TestCreateComponent } from './test-create/test-create.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { CreateExamSectionComponent } from './create-exam-section/create-exam-section.component';
import { ReportsComponent } from './reports/reports.component';
import { CalendarModule } from 'primeng/calendar';
import { DialogModule } from 'primeng/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { KnobModule } from 'primeng/knob';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { TableModule } from 'primeng/table';
import { BadgeModule } from 'primeng/badge';
import { TagModule } from 'primeng/tag';
import { PaginatorModule } from 'primeng/paginator';
import { MessageModule } from 'primeng/message';


@NgModule({
  declarations: [
    HomeComponent,
    UsersDashboardComponent,
    TestHistoryDashboardComponent,
    TestCreateComponent,
    NavbarComponent,
    FooterComponent,
    CreateExamSectionComponent,
    ReportsComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    CardModule,
    ButtonModule,
    CalendarModule,
    DialogModule,
    ReactiveFormsModule,
    DropdownModule,
    KnobModule,
    ToggleButtonModule,
    FormsModule,
    InputNumberModule,
    TableModule,
    BadgeModule,
    TagModule,
    PaginatorModule,
    MessageModule
  ]
})
export class AdminModule { }
