export class User {
    id: number;
    username: string;
    password: string;
    email: string;
    firstname: string;
    lastname: string;
    ts: number;

    constructor() {
        this.id = 0;
        this.username = '';
        this.password = '';
        this.email = '';
        this.firstname = '';
        this.lastname = '';
        this.ts = 0;
    }
}