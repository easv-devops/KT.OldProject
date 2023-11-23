import {HttpClient} from "@angular/common/http"
import {Injectable} from "@angular/core";
import { environment } from "src/environments/environment.prod";
import {Observable} from "rxjs";

export interface Avatar {
  id: number;
  name: string;
  price: number;
}

@Injectable()
export class ProductsService {
  constructor(private readonly http: HttpClient) {
  }

  public getAllProducts(){
    return this.http.get<Avatar[]>("/avatar/all")
  }

}
