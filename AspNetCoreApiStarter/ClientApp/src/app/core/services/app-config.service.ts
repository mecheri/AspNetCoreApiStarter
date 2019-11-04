import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// RxJS
import { map, catchError } from 'rxjs/operators';

// Translation Config Module
import { TranslateService } from '@ngx-translate/core';

// Current env
import { environment } from '../../../environments/environment';

/**
 * App config interface
 *
 * @interface IConfig
 */
interface IConfig {
    version: string,
    apiUrl: string
}

@Injectable()
export class AppConfig {

    private _config: IConfig;
    private _rsc: any;
    private _userCtx: string;
    private _config_path = './assets/config/';
    private _resources_path = './assets/i18n/';

    /**
     * Read settings
     *
     * @readonly
     * @type {IConfig}
     * @memberof AppConfig
     */
    get config(): IConfig {
        return this._config;
    }

    /**
     * Read resources
     *
     * @readonly
     * @type {*}
     * @memberof AppConfig
     */
    get rsc(): any {
        return this._rsc;
    }

    /**
     * Read user context
     *
     * @readonly
     * @type {string}
     * @memberof AppConfig
     */
    get userCtx(): string {
        return this._userCtx;
    }

    /**
     * Creates an instance of AppConfig.
     * @param {HttpClient} http
     * @memberof AppConfig
     */
    constructor(private http: HttpClient) { }

    /**
     * Load app settings, resources and user context
     *
     * @param {TranslateService} translate
     * @returns
     * @memberof AppConfig
     */
    public load(translate: TranslateService) {
        return new Promise((resolve, reject) => {
            this.loadFile(`${this._config_path}${environment.name}.json`).then((conf: any) => {
                this._config = conf;
                this.loadFile(`${this._resources_path}${translate.getBrowserLang()}.json`)
                    .then((resource: any) => {
                        this._rsc = resource;
                        return resolve(true);
                    });
            });
        });
    }

    /**
     * Load file
     *
     * @private
     * @param {string} path
     * @returns {Promise<any>}
     * @memberof AppConfig
     */
    private loadFile(path: string): Promise<any> {
        return new Promise((resolve, reject) => {
            this.http.get(path)
                .pipe(
                    map((res: any) => res),
                    catchError((error) => { throw error; })
                ).subscribe((res_data) => resolve(res_data))
        });
    }
}