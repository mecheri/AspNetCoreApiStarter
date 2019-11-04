import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// RxJS
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// Services
import { AppConfig } from '../../../core/services/app-config.service';

@Injectable({
    providedIn: 'root'
})
export class ReportsService {

    /**
     * Creates an instance of ReportsService.
     * @param {HttpClient} http
     * @param {AppConfig} appConfig
     * @memberof ReportsService
     */
    constructor(
        private http: HttpClient,
        private appConfig: AppConfig,
    ) { }

    getPDF(): Observable<any> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/Reports/Pdf`, { responseType: 'blob' })
            .pipe(map((resp: Blob) => new Blob([resp], { type: 'application/pdf' })));
    }

    getExcel(): Observable<any> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/Reports/Excel`, { responseType: 'blob' })
            .pipe(map((resp: Blob) => new Blob([resp], { type: 'application/vnd.ms-excel' })));
    }

    getExcelNPOI(): Observable<any> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/Reports/ExcelNPOI`, { responseType: 'blob' })
            .pipe(map((resp: Blob) => new Blob([resp], { type: 'application/vnd.ms-excel' })));
    }
}
