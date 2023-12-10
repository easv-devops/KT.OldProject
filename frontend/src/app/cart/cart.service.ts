import {Injectable} from "@angular/core";
import {Avatar} from "../products/products.service";


export interface OrderModel {
  avatar_Array: Avatar[],
  user_id: number;
}

@Injectable({
  providedIn: 'root',
})
export class CartService{




  constructor() {
  }


}
