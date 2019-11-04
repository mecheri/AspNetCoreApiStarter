import { NgModule, Optional, SkipSelf, ErrorHandler, APP_INITIALIZER, LOCALE_ID } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NotifierModule } from 'angular-notifier';

// Prevent re-import of the core module
import { throwIfAlreadyLoaded } from './module-import-guard';

// Interceptors
import { ServerErrorInterceptor } from './interceptors/server-error.interceptor';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { SpinnerInterceptor } from './interceptors/spinner.interceptor';

// Handlers
import { GlobalErrorHandler } from './handlers/global-error.handler';

// Calling load to get configuration + translation
import { AppConfig } from './services/app-config.service';
import { TranslationConfigModule } from './translation.config.module';
import { TranslateService } from '@ngx-translate/core';
export function initResources(config: AppConfig, translate: TranslateService) {
  return () => config.load(translate);
}

@NgModule({
  imports: [
    HttpClientModule,
    TranslationConfigModule,
    NotifierModule.withConfig({
      position: {
        horizontal: {
          position: 'right'
        }
      }
    })
  ],
  exports: [
    HttpClientModule,
    TranslationConfigModule,
    NotifierModule
  ],
  providers: [
    AppConfig, {
      provide: APP_INITIALIZER,
      useFactory: initResources,
      deps: [AppConfig, TranslateService],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ServerErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SpinnerInterceptor,
      multi: true
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    },
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
