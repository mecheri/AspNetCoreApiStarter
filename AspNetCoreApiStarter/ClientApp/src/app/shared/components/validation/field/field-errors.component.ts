import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'app-field-errors',
    templateUrl: './field-errors.component.html',
    styleUrls: ['./field-errors.component.scss']
})
export class FieldErrorsComponent {
    @Input() control: FormControl;
    @Input() clientErrorMessages?: Object;
    @Input() serverErrorMessages?: string[];

    get clientErrorMessage() {
        if (this.control) {
            for (let propertyName in this.control.errors) {
                if (this.control.errors.hasOwnProperty(propertyName) && this.control.touched) {
                    return this.clientErrorMessages ? this.clientErrorMessages[propertyName] : null;
                }
            }
        }

        return null;
    }

    get serverErrorMessage() {
        return this.serverErrorMessages ? this.serverErrorMessages[0] : null;
    }
}