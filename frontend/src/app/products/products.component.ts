import { Component, OnInit } from "@angular/core";
import { User, HomeService } from './products.service';


@Component({
  template: `
    <ion-card>
      <ion-card-header>
        <ion-card-title>Avatars</ion-card-title>
        <ion-card-subtitle>All Avatars 5€!</ion-card-subtitle>
      </ion-card-header>
      <ion-card-content>
        <ion-list>
          <ion-item >
            <ion-thumbnail slot="start">
              <img src="https://robohash.org/abc.png" />
            </ion-thumbnail>
            <ion-label>Tars</ion-label>
            <ion-card-subtitle>5€</ion-card-subtitle>
            <ion-button fill="clear">
              <ion-icon name="cart-outline"> 5€ </ion-icon>
            </ion-button>
          </ion-item>

          <ion-item>
            <ion-thumbnail slot="start">
              <img src="https://robohash.org/def.png" />
            </ion-thumbnail>
            <ion-label>C-3P0</ion-label>
            <ion-card-subtitle>5€</ion-card-subtitle>
            <ion-button fill="clear">
              <ion-icon name="cart-outline"> 5€ </ion-icon>
            </ion-button>
          </ion-item>

          <ion-item>
            <ion-thumbnail slot="start">
              <img src="https://robohash.org/ghi.png" />
            </ion-thumbnail>
            <ion-label>WALL-E</ion-label>
            <ion-card-subtitle>5€</ion-card-subtitle>
            <ion-button fill="clear">
              <ion-icon name="cart-outline"> 5€ </ion-icon>
            </ion-button>
          </ion-item>

          <ion-item lines="none">
            <ion-thumbnail slot="start">
              <img src="https://robohash.org/jkl.png" />
            </ion-thumbnail>
            <ion-label>ROBOCOP</ion-label>
            <ion-card-subtitle>5€</ion-card-subtitle>
            <ion-button fill="clear">
              <ion-icon name="cart-outline"> 5€ </ion-icon>
            </ion-button>
          </ion-item>
        </ion-list>
      </ion-card-content>
    </ion-card>

  `
})
export class ProductsComponent implements OnInit {


  constructor(private readonly service: HomeService) { }

  ngOnInit(): void {

  }
}
