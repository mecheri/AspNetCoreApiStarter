import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../../material.module'
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedModule } from '../../shared/shared.module';

// Routing
import { UsersRoutingModule } from './users-routing.module';

// Components
import { UsersComponent } from './components/users.component';
import { UserNewComponent } from './components/new/user-new.component';
import { UserEditComponent } from './components/edit/user-edit.component';
import { UserDeleteComponent } from './components/delete/user-delete.component';

// Sample with server side validation
import { UserNewWithServerValidationComponent } from './components/new/user-new-with-server-validation.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FlexLayoutModule,
    SharedModule,
    UsersRoutingModule,
  ],
  declarations: [
    UsersComponent,
    UserNewComponent,
    UserEditComponent,
    UserDeleteComponent,
    UserNewWithServerValidationComponent
  ],
  entryComponents: [UserDeleteComponent]
})
export class UsersModule { }
