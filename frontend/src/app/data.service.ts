import {Injectable} from "@angular/core";
import {BehaviorSubject} from "rxjs";
import {Avatar} from "./products/products.service";


@Injectable({providedIn: 'root'})
export class DataService{
  private numberScource = new BehaviorSubject<Avatar | undefined>(undefined);

  currentNumber = this.numberScource.asObservable();

  avatar: Avatar | undefined

  changeAvatar(avatarElement: Avatar)
  {
    this.avatar=avatarElement
    this.numberScource.next(avatarElement)
  }
}
