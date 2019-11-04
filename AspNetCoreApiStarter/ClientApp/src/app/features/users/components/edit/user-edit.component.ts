import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
    FormBuilder,
    FormGroup,
    Validators,
    FormControl
} from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { NotifierService } from 'angular-notifier';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user';

import { cloneDeep } from 'lodash';
import { AppConfig } from '../../../../core/services/app-config.service';

@Component({
    selector: 'app-user-edit',
    templateUrl: './user-edit.component.html',
    styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
    public rsc: any;
    public user: User;
    public form: FormGroup;
    public isFormSaved: boolean;

    /**
     * Creates an instance of UserEditComponent.
     * @param {Router} router
     * @param {ActivatedRoute} route
     * @param {FormBuilder} fb
     * @param {NotifierService} notifier
     * @param {AppConfig} appConfig
     * @param {UsersService} usersService
     * @memberof UserEditComponent
     */
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private fb: FormBuilder,
        private notifier: NotifierService,
        private appConfig: AppConfig,
        private usersService: UsersService
    ) { }

    ngOnInit() {
        this.rsc = this.appConfig.rsc.pages.users.edit;
        this.createForm();
        this.loadUser();
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

    loadUser() {
        const id = +this.route.snapshot.paramMap.get('id');
        this.usersService.getUser(id)
            .subscribe(
                user => {
                    this.user = user;
                    this.form.patchValue(this.user);
                    this.form.controls['confirmPassword'].setValue(user.password);
                }
            );
    }

    onCancel() {
        this.router.navigate(['/users']);
    }

    onSubmit() {
        const user = cloneDeep(this.form.value);
        this.usersService.updateUser(user)
            .subscribe(
                resp => {
                    this.isFormSaved = true;
                    this.notifier.notify('success', 'Operation successfully done !');
                    this.router.navigate(['users']);
                }
            );
    }
}
