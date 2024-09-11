import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TestComponent } from './test/test.component';
import { TestInstructionComponent } from './test-instruction/test-instruction.component';
import { ProfileComponent } from './profile/profile.component';
import { PageNotFoundComponent } from '../shared/page-not-found/page-not-found.component';
import { ResultComponent } from './result/result.component';
import { ChooseTestComponent } from './choose-test/choose-test.component';
import { TestHistoryComponent } from './test-history/test-history.component';

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
    path: 'choose-exam',
    component: ChooseTestComponent
  },
  {
    path: 'exam',
    component: TestComponent
  },
  {
    path: 'exam-start',
    component: TestInstructionComponent
  },
  {
    path: 'profile',
    component: ProfileComponent
  },
  {
    path: 'result',
    component:ResultComponent
  },
  {
    path: 'test-history',
    component: TestHistoryComponent
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
export class UserRoutingModule { }
