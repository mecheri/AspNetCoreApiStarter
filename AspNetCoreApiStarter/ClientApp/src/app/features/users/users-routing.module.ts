import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { UsersComponent } from './components/users.component';
import { UserNewComponent } from './components/new/user-new.component';
import { UserEditComponent } from './components/edit/user-edit.component';

// Sample with server side validation
import { UserNewWithServerValidationComponent } from './components/new/user-new-with-server-validation.component';

const usersRoutes: Routes = [
  {
    path: '',
    component: UsersComponent
  },
  {
    path: 'new',
    component: UserNewWithServerValidationComponent
  },
  {
    path: 'edit/:id',
    component: UserEditComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(usersRoutes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
