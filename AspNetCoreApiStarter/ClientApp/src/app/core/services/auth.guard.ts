import { Injectable } from '@angular/core';
import { Router, CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';

// RxJS
import { Observable } from 'rxjs';

// Services
import { AuthService } from './auth.service';


@Injectable({
    providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild {

    /**
     * Creates an instance of AuthGuard.
     * @param {Router} router
     * @param {AuthService} authService
     * @memberof AuthGuard
     */
    constructor(
        private router: Router,
        private authService: AuthService
    ) { }

    /**
     * Indicates if a route can be activated
     *
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {boolean}
     * @memberof AuthGuardService
     */
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let url: string = state.url;
        return this.checkLogin(url);
    }

    /**
     * Indicates if a child route can be activated
     *
     * @param {ActivatedRouteSnapshot} childRoute
     * @param {RouterStateSnapshot} state
     * @returns {(boolean | Observable<boolean> | Promise<boolean>)}
     * @memberof AuthGuard
     */
    canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
        let url: string = state.url;
        return this.checkLogin(url);
    }

    /**
     * Check if user is logged in
     *
     * @param {string} url
     * @returns {boolean}
     * @memberof AuthGuardService
     */
    checkLogin(url: string): boolean {
        if (this.authService.isLoggedIn()) { return true; }

        // Store the attempted URL for redirecting
        this.authService.redirectUrl = url;

        // Navigate to the login page with extras
        this.router.navigate(['authentication', 'login']);
        return false;
    }
}