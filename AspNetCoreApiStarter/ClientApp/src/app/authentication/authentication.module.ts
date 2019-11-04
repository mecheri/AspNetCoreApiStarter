import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './../material.module'
import { FlexLayoutModule } from '@angular/flex-layout';

// Routing
import { AuthenticationRoutingModule } from './authentication-routing.module';

// Components
import { LoginComponent } from './components/login/login.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FlexLayoutModule,
    AuthenticationRoutingModule
  ],
  declarations: [
    LoginComponent
  ]
})
export class AuthenticationModule { }
