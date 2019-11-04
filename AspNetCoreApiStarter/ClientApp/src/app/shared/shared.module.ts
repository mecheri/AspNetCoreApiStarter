import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MaterialModule } from './../material.module'
import { FlexLayoutModule } from '@angular/flex-layout';

import { ErrorComponent } from './components/error/error.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { FieldErrorsComponent } from './components/validation/field/field-errors.component';
import { FormErrorsComponent } from './components/validation/form/form-errors.component';

import { AccordionAnchorDirective, AccordionLinkDirective, AccordionDirective } from './directives/accordion';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,
    FlexLayoutModule
  ],
  declarations: [
    ErrorComponent,
    NotFoundComponent,
    SpinnerComponent,
    FieldErrorsComponent,
    FormErrorsComponent,
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
  ],
  exports: [
    ErrorComponent,
    NotFoundComponent,
    SpinnerComponent,
    FieldErrorsComponent,
    FormErrorsComponent,
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective
  ]
})
export class SharedModule { }
