import { Component } from "@angular/core";

@Component({
  template: `
      <ion-tabs>
          <ion-tab-bar slot="bottom">
              <ion-tab-button tab="products">
                  <ion-icon name="reader-outline"></ion-icon>
                  Products
              </ion-tab-button>
              <ion-tab-button tab="cart">
                  <ion-icon name="cart-outline"></ion-icon>
                  Cart
              </ion-tab-button>
              <ion-tab-button tab="account">
                  <ion-icon name="person-outline"></ion-icon>
                  Account
              </ion-tab-button>
          </ion-tab-bar>
      </ion-tabs>
  `
})
export class TabsComponent {}
