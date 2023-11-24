import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

export interface User {
    id: number;
    fullName: string;
    email: string;
    isAdmin: boolean;
}

export interface Credentials {
    email: string;
    password: string;
}

export interface Registration {
    fullName: string;
    address: string;
    zipcode: number;
    email: string;
    password: string;
}

@Injectable()
export class AccountService {
    constructor(private readonly http: HttpClient) { }

    getCurrentUser() {
        return this.http.get<User>('/api/account/getinfo');
    }

    login(value: Credentials) {
        return this.http.post<{ token: string }>('/api/account/login', value);
    }
}
