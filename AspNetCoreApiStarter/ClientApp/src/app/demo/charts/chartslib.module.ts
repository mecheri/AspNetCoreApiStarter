import 'hammerjs';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { CommonModule } from '@angular/common';

import { MaterialModule } from '../../material.module';
import { CdkTableModule } from '@angular/cdk/table';
import { ChartsModule } from 'ng2-charts';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';

import { ChartsRoutes } from './chartslib.routing';
import { ChartistModule } from 'ng-chartist';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { ChartjsComponent } from './chart-js/chartjs.component';
import { ChartistjsComponent } from './chartist-js/chartistjs.component';
import { NgxchartComponent } from './ngx-charts/ngx-chart.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ChartsRoutes),
    MaterialModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    CdkTableModule,
    ChartistModule,
    ChartsModule,
    NgxChartsModule
  ],
  providers: [],
  declarations: [ChartjsComponent, ChartistjsComponent, NgxchartComponent]
})
export class ChartslibModule { }
