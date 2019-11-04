import { Routes } from '@angular/router';

import { ChartjsComponent } from './chart-js/chartjs.component';
import { ChartistjsComponent } from './chartist-js/chartistjs.component';
import { NgxchartComponent } from './ngx-charts/ngx-chart.component';

export const ChartsRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'chartjs',
        component: ChartjsComponent
      },
      {
        path: 'chartistjs',
        component: ChartistjsComponent
      },
      {
        path: 'ngxchart',
        component: NgxchartComponent
      }
    ]
  }
];
