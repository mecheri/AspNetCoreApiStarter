import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { NotifierService } from 'angular-notifier';
import { AppConfig } from '../../../../core/services/app-config.service';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-user-delete',
  templateUrl: './user-delete.component.html',
  styleUrls: ['./user-delete.component.scss']
})
export class UserDeleteComponent implements OnInit {
  rsc: any;

  /**
   * Creates an instance of UserDeleteComponent.
   * @param {NotifierService} notifier
   * @param {AppConfig} appConfig
   * @param {UsersService} usersService
   * @param {MatDialogRef<UserDeleteComponent>} dialogRef
   * @param {User} user
   * @memberof UserDeleteComponent
   */
  constructor(
    private notifier: NotifierService,
    private appConfig: AppConfig,
    private usersService: UsersService,
    public dialogRef: MatDialogRef<UserDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public user: User
  ) { }

  ngOnInit() { }

  onCancel() {
    this.dialogRef.close(false);
  }

  onDelete() {
    this.usersService.deleteUser(this.user.id)
      .subscribe(
        (resp) => {
          this.dialogRef.close(true);
          this.notifier.notify('success', 'Operation successfully done !');
        }
      );
  }
}
