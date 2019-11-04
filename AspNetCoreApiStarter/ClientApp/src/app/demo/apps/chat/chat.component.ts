import { Component } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {

  them;
  user;
  sidePanelOpened = true;

  // MESSAGE
  selectedMessage: any;
  messages: Object[] = [{
      from: 'Nirav Joshi',
      photo: 'assets/images/users/1.jpg',
      subject: 'Hey, how are you?',
    }, {
      from: 'Sunil Joshi',
      photo: 'assets/images/users/2.jpg',
      subject: 'Lorem ipsum done dkaghdka',
    }, {
      from: 'Vishal bhatt',
      photo: 'assets/images/users/3.jpg',
      subject: 'Thanks mate',
    }, {
      from: 'Genelia Desouza',
      photo: 'assets/images/users/4.jpg',
      subject: 'This is my shot',
    }, {
      from: 'Linda muke',
      photo: 'assets/images/users/5.jpg',
      subject: 'You have to do it with your self',
    }, {
      from: 'Vaibhav Zala',
      photo: 'assets/images/users/6.jpg',
      subject: 'No mate this is not',
    }, {
      from: 'Kalu valand',
      photo: 'assets/images/users/1.jpg',
      subject: 'Arti thai gai ne?',
    }];

  constructor() {
    this.selectedMessage = this.messages[1];
  }

  isOver(): boolean {
    return window.matchMedia(`(max-width: 960px)`).matches;
  }

  onSelect(message: Object[]): void {
    this.selectedMessage = message;
  }

}
