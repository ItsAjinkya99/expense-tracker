import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideIcons } from '@ng-icons/core';
import { lucideChartPie, lucideReceipt, lucideWallet } from '@ng-icons/lucide';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideIcons({ lucideChartPie, lucideReceipt, lucideWallet }),
  ],
};
