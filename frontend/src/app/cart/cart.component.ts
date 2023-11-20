import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  template: `
    <app-title [title]="'Cart'"></app-title>

    <ion-content>

      <ion-label> spidd up</ion-label>


    </ion-content>
    <ion-label> spidd up</ion-label>


  `
})
export class CartComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
