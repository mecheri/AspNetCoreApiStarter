import 'hammerjs';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TablesRoutes } from './tables.routing';

import { Ng2SmartTableModule } from 'ng2-smart-table';

import { BasicTableComponent } from './basic-table/basic-table.component';
import { FilterableComponent } from './filterable/filterable.component';
import { PaginationComponent } from './pagination/pagination.component';
import { SmarttableComponent } from './smart-table/smart-table.component';
import { SortableComponent } from './sortable/sortable.component';
import { MixComponent } from './mix/mix.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(TablesRoutes),
    MaterialModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SmartTableModule
  ],
  declarations: [
    BasicTableComponent,
    FilterableComponent,
    PaginationComponent,
    SortableComponent,
    MixComponent,
    SmarttableComponent
  ]
})
export class TablesModule { }
