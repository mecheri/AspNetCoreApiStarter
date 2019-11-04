import { HttpClient } from '@angular/common/http';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { isNull, isUndefined } from 'lodash';

export function HttpLoaderFactory(http: HttpClient) {
    return new TranslateHttpLoader(http, '../../../assets/i18n/', '.json');
}

const translationOptions = {
    loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
    }
};

@NgModule({
    imports: [TranslateModule.forRoot(translationOptions)],
    exports: [TranslateModule],
    providers: [TranslateService]
})
export class TranslationConfigModule {

    private browserLang;

    /**
     * Creates an instance of TranslationConfigModule.
     * @param {TranslateService} translate
     * @memberof TranslationConfigModule
     */
    constructor(private translate: TranslateService) {
        // Setting up Translations
        translate.addLangs(['fr', 'en']);
        translate.setDefaultLang('fr');
        this.browserLang = translate.getBrowserLang();
        translate.use(this.browserLang.match(/fr|en/) ? this.browserLang : 'fr');
    }
}