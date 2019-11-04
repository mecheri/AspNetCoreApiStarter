import { Component, OnInit } from '@angular/core';

// Services
import { ReportsService } from '../services/reports.service';
import { MixinService } from '../../../core/services/mixin.service';
import { HttpResponse } from '@angular/common/http';

@Component({
    selector: 'app-reports',
    templateUrl: './reports.component.html',
    styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {

    /**
     * Creates an instance of ReportsComponent.
     * @param {MixinService} mixinService
     * @param {ReportsService} reportsService
     * @memberof ReportsComponent
     */
    constructor(
        private mixinService: MixinService,
        private reportsService: ReportsService
    ) { }

    ngOnInit(): void { }

    downloadPDF() {
        this.reportsService.getPDF()
            .subscribe(
                (report: Blob) => {
                    if (this.mixinService.isIE()) {
                        window.navigator.msSaveOrOpenBlob(report);
                    } else {
                        window.open(URL.createObjectURL(report));
                    }
                });
    }

    downloadExcel() {
        this.reportsService.getExcel()
            .subscribe(
                (blob: Blob) => {
                    const filename = 'DemoOPENXML.xlsx';
                    if (this.mixinService.isIE()) {
                        window.navigator.msSaveOrOpenBlob(blob, filename);
                    } else {
                        const anchor = document.createElement("a");
                        anchor.href = URL.createObjectURL(blob);
                        anchor.download = filename;
                        anchor.click();
                    }
                });
    }

    downloadExcelNPOI() {
        this.reportsService.getExcelNPOI()
            .subscribe(
                (blob: Blob) => {
                    const filename = 'DemoNPOI.xlsx';
                    if (this.mixinService.isIE()) {
                        window.navigator.msSaveOrOpenBlob(blob, filename);
                    } else {
                        const anchor = document.createElement("a");
                        anchor.href = URL.createObjectURL(blob);
                        anchor.download = filename;
                        anchor.click();
                    }
                });
    }
}
