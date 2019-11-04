import { Routes } from '@angular/router';

import { DataTableComponent } from './data-table/data-table.component';
import { TableEditingComponent } from './table-editing/table-editing.component';
import { TableFilterComponent } from './table-filter/table-filter.component';
import { MaterialTableComponent } from './materialtable/materialtable.component';
export const DataTablesRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'basicdatatable',
        component: DataTableComponent
      },
      {
        path: 'editing',
        component: TableEditingComponent
      },
      {
        path: 'filter',
        component: TableFilterComponent
      },
      {
        path: 'materialtable',
        component: MaterialTableComponent
      }
    ]
  }
];
