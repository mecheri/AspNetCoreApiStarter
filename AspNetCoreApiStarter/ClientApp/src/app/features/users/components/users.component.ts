import { Component, ViewChild, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatPaginator, MatTableDataSource, MatSort, MatDialog } from '@angular/material';
import { BreakpointObserver } from '@angular/cdk/layout';

// Services
import { UsersService } from '../services/users.service';
import { AppConfig } from '../../../core/services/app-config.service';

// Components
import { UserDeleteComponent } from './delete/user-delete.component';

// Models
import { User } from './../models/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  rsc: any;
  displayedColumns = ['id', 'username', 'firstname', 'lastname', 'actions'];
  dataSource: MatTableDataSource<User>;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  /**
   * Creates an instance of UsersComponent.
   * @param {Router} router
   * @param {MatDialog} dialog
   * @param {BreakpointObserver} breakpointObserver
   * @param {AppConfig} appConfig
   * @param {UsersService} usersService
   * @memberof UsersComponent
   */
  constructor(
    private router: Router,
    private dialog: MatDialog,
    private breakpointObserver: BreakpointObserver,
    private appConfig: AppConfig,
    private usersService: UsersService,
  ) {
    this.breakpointObserver.observe(['(max-width: 600px)']).subscribe(result => {
      this.displayedColumns = result.matches ?
        ['id', 'username', 'firstname', 'lastname', 'actions'] :
        ['id', 'username', 'firstname', 'lastname', 'actions'];
    });
  }

  ngOnInit(): void {
    this.rsc = this.appConfig.rsc.pages.users.table;
    this.loadUsers()
  }

  loadUsers() {
    this.usersService.getUsers()
      .subscribe((users: User[]) => {
        this.dataSource = new MatTableDataSource<User>(users);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      });
  }

  onAddUser() {
    this.router.navigate(['users', 'new']);
  }

  onEditUser(user: User) {
    this.router.navigate(['users', 'edit', user.id]);
  }

  onDeleteUser(user: User) {
    const dialogRef = this.dialog.open(UserDeleteComponent, {
      width: '600px',
      data: user
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) { this.loadUsers(); }
    });
  }

 
  private toto(test: boolean, obj: any) : boolean {
    // TODO 
   return true
  } 
}
