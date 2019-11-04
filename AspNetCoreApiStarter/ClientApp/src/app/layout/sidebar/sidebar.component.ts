import {
  ChangeDetectorRef,
  Component,
  NgZone,
  OnDestroy,
  ViewChild,
  HostListener,
  Directive,
  AfterViewInit,
  OnInit
} from '@angular/core';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { MediaMatcher } from '@angular/cdk/layout';
import { AppConfig } from '../../core/services/app-config.service';
import { IMenu } from '../../shared/models/menu';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: []
})
export class AppSidebarComponent implements OnInit, OnDestroy {
  config: PerfectScrollbarConfigInterface = {};
  mobileQuery: MediaQueryList;
  menuItems: IMenu[];
  rsc: any;

  private _mobileQueryListener: () => void;
  status: boolean = false;

  clickEvent() {
    this.status = !this.status;
  }

  subclickEvent() {
    this.status = true;
  }

  /**
   * Creates an instance of AppSidebarComponent.
   * @param {ChangeDetectorRef} changeDetectorRef
   * @param {MediaMatcher} media
   * @param {AppConfig} appConfig
   * @param {AuthService} authService
   * @memberof AppSidebarComponent
   */
  constructor(
    private changeDetectorRef: ChangeDetectorRef,
    private media: MediaMatcher,
    private appConfig: AppConfig,
    private authService: AuthService
  ) {
    this.mobileQuery = this.media.matchMedia('(min-width: 768px)');
    this._mobileQueryListener = () => this.changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  ngOnInit() {
    this.rsc = this.appConfig.rsc.layout.leftsidebar;
    this.menuItems = this.rsc.menuItems;
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  signOut() {
    this.authService.logout();
  }
}
