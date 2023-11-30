import {HttpClient} from "@angular/common/http"
import {Injectable} from "@angular/core";
import { environment } from "src/environments/environment.prod";

export interface ResponseDto<T> {
  responseData: T;
}
export interface Avatar {
  avatar_id: number;
  avatar_name: string;
  avatar_price: number;
}

@Injectable()
export class ProductsService {
  constructor(private readonly http: HttpClient) {
  }

  public getAllProducts(){
    return this.http.get<ResponseDto<Avatar[]>>(environment.baseUrl + "/avatar/all")
  }

}
