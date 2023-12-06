import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

export interface User {
    user_id: number;
    full_name: string;
    email: string;
<<<<<<< Updated upstream
    admin: string;
=======
    Admin: string;
>>>>>>> Stashed changes
}

export interface Credentials {
    email: string;
    password: string;
}

export interface Registration {
    full_name: string;
    email: string;
    password: string;
}

export class ResponseDto<T> {
  responseData?: T;
  messageToClient?: string;
}

@Injectable()
export class AccountService {
    constructor(private readonly http: HttpClient) { }

    getCurrentUser() {
        return this.http.get<User>('/api/account/getinfo');
    }

    loginToAccount(value: Credentials) {
        return this.http.post<{ token: string }>('/api/account/login', value);
    }
}
