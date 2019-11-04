import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
    FormBuilder,
    FormGroup,
    Validators,
    FormControl
} from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { NotifierService } from 'angular-notifier';
import { AppConfig } from '../../../../core/services/app-config.service';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user';

import { cloneDeep } from 'lodash';


@Component({
    selector: 'app-user-new',
    templateUrl: './user-new.component.html',
    styleUrls: ['./user-new.component.scss']
})
export class UserNewComponent implements OnInit {
    public rsc: any;
    public form: FormGroup;

    /**
     * Creates an instance of UserNewComponent.
     * @param {Router} router
     * @param {FormBuilder} fb
     * @param {NotifierService} notifier
     * @param {AppConfig} appConfig
     * @param {UsersService} usersService
     * @memberof UserNewComponent
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
        this.form.controls.username.setValidators(Validators.required);
        this.form.controls.email.setValidators(Validators.compose([Validators.required, CustomValidators.email]));
        this.form.controls.firstname.setValidators(Validators.required);
        this.form.controls.lastname.setValidators(Validators.required);
        this.form.controls.password.setValidators(Validators.required);
        this.form.addControl('confirmPassword', new FormControl());
        this.form.controls.confirmPassword.setValidators(CustomValidators.equalTo(this.form.controls.password));
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
                });
    }
}
