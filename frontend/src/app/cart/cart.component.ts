import { Component, OnInit } from '@angular/core';
import {Avatar} from "../products/products.service";
import {ToastController} from "@ionic/angular";
import {Router} from "@angular/router";
import {ResponseDto, TokenResponse} from "../account/login.component";
import {HttpClient} from "@angular/common/http";
import {OrderModel} from "./cart.service";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-cart',
  template: `

    <app-title title="CART"></app-title>
    <ion-content color="light">
      <ion-button class="button" (click)="checkOut()" fill="clear" > CHECKOUT
        <ion-icon name="cart-outline"></ion-icon>
      </ion-button>
      <ion-label>{{totalPrice}} ¢</ion-label>
      <ion-list inset="true" *ngFor="let avatar of myArr; index as i">
        <ion-item>
          <img src="https://robohash.org/{{avatar.avatar_name}}.png" height="150px" width="150px"/>
          <ion-label>{{avatar.avatar_name}}</ion-label>
          <ion-label>{{avatar.avatar_price}} €</ion-label>
          <ion-button class="button" (click)="removeAvatar(avatar)" fill="clear" > REMOVE</ion-button>
        </ion-item>
      </ion-list>

    </ion-content>

  `
})
export class CartComponent  implements OnInit {

  jsonArray: any;
  myArr: Avatar[];
  totalPrice: number;

  constructor(private readonly toast: ToastController, private readonly router: Router, private readonly http: HttpClient,) {

    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };

    this.jsonArray = sessionStorage.getItem("cart");
    this.myArr = JSON.parse(this.jsonArray);
    this.totalPrice = 0;

  }

  ngOnInit() {
    if (this.myArr != null) {
      for (let avatar of this.myArr) {
        var price = avatar.avatar_price;
        this.totalPrice = this.totalPrice + price
      }
    }
  }

  removeAvatar(avatar: Avatar) {

    if (this.myArr.length === 1) {
      sessionStorage.removeItem("cart");
      this.myArr.pop();

    } else {

      let toBeDeleted = this.myArr.findIndex((ava, index) => {
        ava.avatar_name === avatar.avatar_name;
        return index;
      })

      let newArr: Avatar[] = this.myArr.splice(toBeDeleted, 1);
      sessionStorage.setItem("cart", JSON.stringify(newArr));
    }
  }

  public checkOut() {

    let order = {array: sessionStorage.getItem("cart"), id: 2}

    console.log(order.id);
    console.log(order.array);

    this.http.post(environment.baseUrl + 'api/orderWithProducts', order);

    console.log("I was Called!");


    /*
        (await this.toast.create({
          message: 'Order Confirmed! Check your e-mail!',
          color: 'success',
          duration: 5000,
          icon: 'success',
        })).present();

        sessionStorage.removeItem("cart");
        setTimeout(() => {
          document.location.reload();
        }, 3000);
      }
      */

  }
}
