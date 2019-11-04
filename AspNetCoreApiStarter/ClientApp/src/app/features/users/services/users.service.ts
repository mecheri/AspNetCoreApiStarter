import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// RxJS
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// Services
import { AppConfig } from '../../../core/services/app-config.service';

// Models
import { User } from '../models/user';

@Injectable({
    providedIn: 'root'
})
export class UsersService {

    constructor(
        private http: HttpClient,
        private appConfig: AppConfig,
    ) { }

    getUsers(): Observable<User[]> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/Users`)
            .pipe(map((resp) => resp as User[]));
    }

    getUser(id: number): Observable<User> {
        return this.http
            .get(`${this.appConfig.config.apiUrl}/api/Users/${id}`)
            .pipe(map((resp) => resp as User));
    }

    createUser(user: User): Observable<any> {
        return this.http
            .post(`${this.appConfig.config.apiUrl}/api/Users`, user);
    }

    updateUser(user: User): Observable<any> {
        return this.http
            .put(`${this.appConfig.config.apiUrl}/api/Users/${user.id}`, user);
    }

    deleteUser(id: number): Observable<any> {
        return this.http
            .delete(`${this.appConfig.config.apiUrl}/api/Users/${id}`);
    }
}
