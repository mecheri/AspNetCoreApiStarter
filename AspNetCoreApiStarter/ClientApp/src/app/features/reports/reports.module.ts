import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module'
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedModule } from '../../shared/shared.module';

// Routing
import { ReportsRoutingModule } from './reports-routing.module';

// Components
import { ReportsComponent } from './components/reports.component';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FlexLayoutModule,
    SharedModule,
    ReportsRoutingModule,
  ],
  declarations: [
    ReportsComponent,
  ],
})
export class ReportsModule { }
