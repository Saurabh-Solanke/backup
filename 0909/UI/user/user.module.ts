import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { HomeComponent } from './home/home.component';
import { TestHistoryComponent } from './test-history/test-history.component';
import { ProfileComponent } from './profile/profile.component';
import { TestInstructionComponent } from './test-instruction/test-instruction.component';
import { TestComponent } from './test/test.component';
import { ResultComponent } from './result/result.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterLink } from '@angular/router';
import { ChooseTestComponent } from './choose-test/choose-test.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { TagModule } from 'primeng/tag';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TableModule } from 'primeng/table';


@NgModule({
  declarations: [
    HomeComponent,
    TestHistoryComponent,
    ProfileComponent,
    TestInstructionComponent,
    TestComponent,
    ResultComponent,
    NavbarComponent,
    FooterComponent,
    ChooseTestComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    RouterLink,
    CardModule,
    ButtonModule,
    FormsModule,
    TagModule,
    MessageModule,
    ProgressSpinnerModule,
    TableModule
  ]
})
export class UserModule { }
