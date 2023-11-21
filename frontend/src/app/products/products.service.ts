import {HttpClient} from "@angular/common/http"
import {Injectable} from "@angular/core";

export interface User {
  id: number,
  fullName: string
  avatarUrl?: string
}

@Injectable()
export class HomeService {
  constructor(private readonly http: HttpClient) {
  }

}
