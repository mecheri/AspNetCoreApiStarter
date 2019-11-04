import { Routes } from '@angular/router';

import { BasicTableComponent } from './basic-table/basic-table.component';
import { FilterableComponent } from './filterable/filterable.component';
import { PaginationComponent } from './pagination/pagination.component';
import { SortableComponent } from './sortable/sortable.component';
import { MixComponent } from './mix/mix.component';
import { SmarttableComponent } from './smart-table/smart-table.component';
export const TablesRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'basictable',
        component: BasicTableComponent
      },
      {
        path: 'filterable',
        component: FilterableComponent
      },
      {
        path: 'pagination',
        component: PaginationComponent
      },
      {
        path: 'sortable',
        component: SortableComponent
      },
      {
        path: 'mix',
        component: MixComponent
      },
      {
        path: 'smarttable',
        component: SmarttableComponent
      }
    ]
  }
];
