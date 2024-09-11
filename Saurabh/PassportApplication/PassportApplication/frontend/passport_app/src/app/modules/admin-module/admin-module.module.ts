import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FeedbackComplaintTypePipe } from './Pipes/feedback-complaint-type.pipe';
import { ResolvedStatusPipe } from './Pipes/resolved-status.pipe';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: []
})
export class AppModule { }
