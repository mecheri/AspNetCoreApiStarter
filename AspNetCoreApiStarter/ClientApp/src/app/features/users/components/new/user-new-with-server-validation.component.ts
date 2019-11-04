import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, AbstractControl } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { AppConfig } from '../../../../core/services/app-config.service';
import { User } from '../../models/user';

import { cloneDeep } from 'lodash';
import { UsersService } from '../../services/users.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-user-new',
    templateUrl: './user-new-with-server-validation.component.html',
    styleUrls: ['./user-new-with-server-validation.component.scss']
})
export class UserNewWithServerValidationComponent implements OnInit {
    public rsc: any;
    public form: FormGroup;

    /**
     * Creates an instance of UserNewWithServerValidationComponent.
     * @param {Router} router
     * @param {FormBuilder} fb
     * @param {NotifierService} notifier
     * @param {AppConfig} appConfig
     * @param {UsersService} usersService
     * @memberof UserNewWithServerValidationComponent
     */
    constructor(
        private router: Router,
        private fb: FormBuilder,
        private notifier: NotifierService,
        private appConfig: AppConfig,
        private usersService: UsersService
    ) { }

    ngOnInit() {
        this.rsc = this.appConfig.rsc.pages.users.new;
        this.createForm();
    }

    createForm() {
        if (this.form) { this.form.reset(); }
        this.form = this.fb.group(new User());
    }

    onCancel() {
        this.router.navigate(['/users']);
    }

    onSubmit() {
        const user = cloneDeep(this.form.value);
        this.usersService.createUser(user)
            .subscribe(
                () => {
                    this.notifier.notify('success', 'Operation successfully done');
                    this.router.navigate(['users']);
                },
                (errResp: HttpErrorResponse) => this.handleSubmitError(errResp, this.form)
            );
    }

    handleSubmitError(resp: HttpErrorResponse, form: FormGroup): void {
        if (resp.status === 400) {
            const errors = resp.error.validation;
            const fields = Object.keys(errors || {});
            fields.forEach((field) => {
                const control = this.findFieldControl(field, form);
                control.setErrors(errors[field]);
            });
        }
    }

    findFieldControl(field: string, form: FormGroup): AbstractControl {
        let control: AbstractControl;
        if (field === 'base') {
            control = form;
        } else if (form.contains(field)) {
            control = form.get(field);
        } else if (field.match(/_id$/) && form.contains(field.substring(0, field.length - 3))) {
            control = form.get(field.substring(0, field.length - 3));
        } else if (field.indexOf('.') > 0) {
            let group = form;
            field.split('.').forEach((f) => {
                if (group.contains(f)) {
                    control = group.get(f);
                    if (control instanceof FormGroup) group = control;
                } else {
                    control = group;
                }
            })
        } else {
            // Field is not defined in form but there is a validation error for it, set it globally
            control = form;
        }
        return control;
    }
}
