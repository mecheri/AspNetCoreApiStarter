import { Routes } from '@angular/router';

import { FullcalendarComponent } from './fullcalendar/fullcalendar.component';
import { MailComponent } from './mail/mail.component';
import { ChatComponent } from './chat/chat.component';
import { TaskboardComponent } from './taskboard/taskboard.component';
export const AppsRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'calendar',
        component: FullcalendarComponent
      },
      {
        path: 'messages',
        component: MailComponent
      },
      {
        path: 'chat',
        component: ChatComponent
      },
      {
        path: 'taskboard',
        component: TaskboardComponent
      }
    ]
  }
];
