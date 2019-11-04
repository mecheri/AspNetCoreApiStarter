import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

// Services
import { Logger } from '../services/logger.service';
import { AuthService } from '../services/auth.service';
import { NotifierService } from 'angular-notifier';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

    constructor(
        // Le ErrorHandler étant créé avant les services, nous utilisons l'injecteur pour les récupérer
        private injector: Injector,
        private zone: NgZone
    ) { }

    /**
     * Intercepte et gère les erreurs coté client et serveur (erreurs JS ou erreurs HTTP)
     *
     * @param {(Error | HttpErrorResponse)} error Erreur interceptée
     * @memberof ErrorsHandler
     */
    handleError(resp: Error | HttpErrorResponse): void {
        const router: Router = this.injector.get(Router);
        const logger: Logger = this.injector.get(Logger);
        const authService: AuthService = this.injector.get(AuthService);
        const notifier: NotifierService = this.injector.get(NotifierService);

        // Log the error anyway
        logger.error(resp);

        if (resp instanceof HttpErrorResponse) {
            // Erreur HTTP => affichage d'une popup avec le message "user-friendly"
            // Erreur serveur ou erreur de connexion
            if (!navigator.onLine) {
                // Pas de connexion à internet
                this.zone.run(() => notifier.notify('warning', 'No Internet Connection'));
            } else {
                if (router.url !== 'login' && [401, 403].includes(resp.status)) {
                    // Erreur HTTP (error.status === 401, 403)
                    // Déconnexion
                    this.zone.run(() => authService.logout());
                } else {
                    // Erreur HTTP (error.status === 404, 400, 500...)
                    // On affiche une alerte à l'utilisateur
                    this.zone.run(() => notifier.notify('error', resp.error.message));
                }
            }
        } else {
            // Erreur JS coté client (Angular Error, ReferenceError...)
            // On redirige l'utilisateur vers la page d'erreur
            this.zone.run(() => router.navigate(['/error'], {
                queryParams: { error: resp.stack }
            }));
        }
    }
}
