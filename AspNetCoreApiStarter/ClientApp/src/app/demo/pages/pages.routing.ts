import { Routes } from '@angular/router';

import { MatIconComponent } from './material-icons/mat-icon.component';
import { TimelineComponent } from './timeline/timeline.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { PricingComponent } from './pricing/pricing.component';
import { HelperComponent } from './helper-classes/helper.component';

export const PagesRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'icons/material',
        component: MatIconComponent
      },
      {
        path: 'timeline',
        component: TimelineComponent
      },
      {
        path: 'invoice',
        component: InvoiceComponent
      },
      {
        path: 'pricing',
        component: PricingComponent
      },
      {
        path: 'helper',
        component: HelperComponent
      }
    ]
  }
];
