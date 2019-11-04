import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControl
} from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { AppConfig } from '../../../core/services/app-config.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public rsc: any;
  public form: FormGroup;
  public errorMessage: any;

  /**
   * Creates an instance of LoginComponent.
   * @param {Router} router
   * @param {FormBuilder} fb
   * @param {AppConfig} appConfig
   * @param {AuthService} authService
   * @memberof LoginComponent
   */
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private appConfig: AppConfig,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.rsc = this.appConfig.rsc.pages.authentication.login;
    this.form = this.fb.group({
      username: [null, Validators.compose([Validators.required])],
      password: [null, Validators.compose([Validators.required])]
    });
  }

  onSubmit() {
    this.authService.check(this.form.value)
      .subscribe(
        () => this.router.navigate(['/']),
        (error) => this.errorMessage = error
      );
  }
}
