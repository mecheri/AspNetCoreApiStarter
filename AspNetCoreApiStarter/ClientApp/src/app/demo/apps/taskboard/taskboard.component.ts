/* tslint:disable:max-line-length */
import { Component } from '@angular/core';

interface Tasks {
  title: string;
  description: string;
  class?: string;
}

interface Taskboard {
  title: string;
  tasks: Tasks[];
  class?: string;
}

@Component({
  selector: 'app-taskboard',
  templateUrl: './taskboard.component.html',
  styleUrls: ['./taskboard.component.scss']
})
export class TaskboardComponent {
  taskboard: Taskboard[] = [
    {
      title: 'To Dos',
      class: 'todos',
      tasks: [
        {
          title: 'Launch new template',
          description: 'Integer posuere erat a ante venenatis dapibus posuere.'
        },
        {
          title: 'Book a Ticket',
          description: 'Blandit tempus porttitor aasfs.'
        },
        {
          title: 'Task review',
          description:
            'Lorem Ipsum, dapibus ac facilisis in, egestas eget quam. Integer posuere erat a ante venenatis dapibus posuere velit aliquet.',
          class: 'task-status-info'
        }
      ]
    },
    {
      title: 'In Progress',
      class: 'inprogress',
      tasks: [
        {
          title: 'Website Design',
          description: 'Integer posuere erat a ante venenatis dapibus posuere.'
        },
        {
          title: 'Angular 5 material',
          description:
            'Lorem Ipsum, dapibus ac facilisis in, egestas eget quam. Integer posuere erat aassg.',
          class: 'task-status-danger'
        },
        {
          title: 'Horizontal Layoutbug',
          description: 'Lorem Ipsum, dapibus ac facilisis in',
          class: 'task-status-info'
        },
        {
          title: 'Error --prod',
          description: 'Lorem Ipsum, dapibus ac facilisis.'
        },
        {
          title: 'Update to angular5',
          description:
            'Dapibus ac facilisis in, egestas eget quam. Integer posuere erat aassg.'
        },
        {
          title: 'Give quatation',
          description:
            'Commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit.',
          class: 'task-status-warning'
        }
      ]
    },
    {
      title: 'Completed',
      class: 'completed',
      tasks: [
        {
          title: 'Design work',
          description:
            'Commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit.'
        },
        {
          title: 'Meeting with team',
          description: 'Dapibus ac facilisis in, egestas eget quam.',
          class: 'task-status-success'
        },
        {
          title: 'Material Pro angular',
          description: 'We have finished working on MaterialPro'
        },
        {
          title: 'Admin wrap converted',
          description: 'We have finished working.'
        },
        {
          title: 'Learning Angular 5',
          description: 'Task is now completed to learn angular5',
          class: 'task-status-success'
        }
      ]
    },
    {
      title: 'On Hold',
      class: 'onhold',
      tasks: [
        {
          title: 'Ugrate to bootsrap 4 beta',
          description: 'Its panding in all template to update'
        },
        {
          title: 'Required more plugins',
          description: 'Client require more plugins to add.'
        },
        {
          title: 'Communication with client',
          description: 'They want to design like minimal way',
          class: 'task-status-danger'
        },
        {
          title: 'Use gradiant or not',
          description: 'Need approval on whether use gradiant or make it plain'
        },
        {
          title: 'Give review on the product',
          description: 'Commodo luctus, nisi erat porttitor lig.',
          class: 'task-status-danger'
        }
      ]
    }
  ];
}
