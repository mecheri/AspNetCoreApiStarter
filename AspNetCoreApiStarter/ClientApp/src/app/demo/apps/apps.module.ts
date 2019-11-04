import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { QuillModule } from 'ngx-quill';
import { CalendarModule, CalendarDateFormatter } from 'angular-calendar';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { DragulaModule } from 'ng2-dragula/ng2-dragula';
import { AppsRoutes } from './apps.routing';
import { FullcalendarComponent } from './fullcalendar/fullcalendar.component';
import {
  MailComponent,
  DialogDataExampleDialogComponent
} from './mail/mail.component';
import { ChatComponent } from './chat/chat.component';
import { CalendarDialogComponent } from './fullcalendar/fullcalendar.component';
import { TaskboardComponent } from './taskboard/taskboard.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AppsRoutes),
    MaterialModule,
    CalendarModule.forRoot(),
    FlexLayoutModule,
    QuillModule,
    DragulaModule,
    PerfectScrollbarModule
  ],
  declarations: [
    FullcalendarComponent,
    MailComponent,
    DialogDataExampleDialogComponent,
    ChatComponent,
    CalendarDialogComponent,
    TaskboardComponent
  ],
  entryComponents: [CalendarDialogComponent, DialogDataExampleDialogComponent]
})
export class AppsModule { }
