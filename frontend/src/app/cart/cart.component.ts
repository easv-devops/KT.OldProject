import { Component, OnInit } from '@angular/core';
import {Avatar} from "../products/products.service";
import {ToastController} from "@ionic/angular";
import {Router} from "@angular/router";

@Component({
  selector: 'app-cart',
  template: `

    <app-title title="CART"></app-title>
    <ion-content>
      <ion-grid [fixed]="true">
        <ion-row>
          <ion-col *ngFor="let avatar of myArr; index as i">
            <ion-card>
              <ion-card-header>
                <ion-card-title class="name">{{avatar.avatar_name}}</ion-card-title>
                <ion-card-title class="price">{{avatar.avatar_price}} € </ion-card-title>
              </ion-card-header>
              <img src="https://robohash.org/{{avatar.avatar_name}}.png" height="150px" width="150px"/>

              <ion-button class="button" (click)="removeAvatar(avatar)" fill="clear" > REMOVE</ion-button>

            </ion-card>
          </ion-col>
        </ion-row>
      </ion-grid>
      <ion-button class="button" (click)="checkOut()" fill="clear" > CHECKOUT
        <ion-icon name="cart-outline"></ion-icon>
      </ion-button>
    </ion-content>

  `
})
export class CartComponent  implements OnInit {

  jsonArray: any;
  myArr : Avatar[];




  constructor(private readonly toast: ToastController, private readonly router: Router) {

    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };

    this.jsonArray = sessionStorage.getItem("cart");
    this.myArr = JSON.parse(this.jsonArray);

  }

  ngOnInit() {


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

  async checkOut() {


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

}
