import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TestCreateComponent } from './test-create/test-create.component';
import { TestHistoryDashboardComponent } from './test-history-dashboard/test-history-dashboard.component';
import { UsersDashboardComponent } from './users-dashboard/users-dashboard.component';
import { PageNotFoundComponent } from '../shared/page-not-found/page-not-found.component';
import { CreateExamSectionComponent } from './create-exam-section/create-exam-section.component';
import { ReportsComponent } from './reports/reports.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'test-create',
    component: TestCreateComponent
  },
  {
    path: 'test-history-dash',
    component: TestHistoryDashboardComponent
  },
  {
    path: 'manage-users-dash',
    component: UsersDashboardComponent
  },
  {
    path: 'create-test',
    component: TestCreateComponent
  },
  {
    path: 'create-exam-section',
    component: CreateExamSectionComponent
  },
  {
    path: 'reports',
    component: ReportsComponent
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
