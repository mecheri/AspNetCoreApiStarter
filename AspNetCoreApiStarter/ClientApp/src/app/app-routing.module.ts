import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppLayoutComponent } from './layout/layout.component';
import { ErrorComponent } from './shared/components/error/error.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { AuthGuard } from './core/services/auth.guard';

export const AppRoutes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
    canActivateChild: [AuthGuard],
    children: [
      {
        path: '',
        redirectTo: '/users',
        pathMatch: 'full'
      },
      {
        path: 'users',
        loadChildren: './features/users/users.module#UsersModule'
      },
      {
        path: 'reports',
        loadChildren: './features/reports/reports.module#ReportsModule'
      },
      {
        path: 'dashboard',
        loadChildren: './demo/dashboard/dashboard.module#DashboardModule'
      },
      {
        path: 'material',
        loadChildren: './demo/material-component/material.module#MaterialComponentsModule'
      },
      {
        path: 'apps',
        loadChildren: './demo/apps/apps.module#AppsModule'
      },
      {
        path: 'forms',
        loadChildren: './demo/forms/forms.module#FormModule'
      },
      {
        path: 'tables',
        loadChildren: './demo/tables/tables.module#TablesModule'
      },
      {
        path: 'datatables',
        loadChildren: './demo/datatables/datatables.module#DataTablesModule'
      },
      {
        path: 'pages',
        loadChildren: './demo/pages/pages.module#PagesModule'
      },
      {
        path: 'widgets',
        loadChildren: './demo/widgets/widgets.module#WidgetsModule'
      },
      {
        path: 'charts',
        loadChildren: './demo/charts/chartslib.module#ChartslibModule'
      },
      {
        path: 'multi',
        loadChildren: './demo/multi-dropdown/multi-dd.module#MultiModule'
      }
    ]
  },
  {
    path: 'authentication',
    loadChildren: './authentication/authentication.module#AuthenticationModule'
  },
  {
    path: 'error',
    component: ErrorComponent
  },
  {
    path: '404',
    component: NotFoundComponent
  },
  {
    path: '**',
    redirectTo: '404'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(AppRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }