import { Component, OnInit } from "@angular/core";
import { User, HomeService } from './products.service';


@Component({
  template: `
    <app-title [title]="'Products'"></app-title>

    <ion-content>

      <ion-label> spidd up</ion-label>


    </ion-content>
    <ion-label> spidd up</ion-label>


  `
})
export class ProductsComponent implements OnInit {


  constructor(private readonly service: HomeService) { }

  ngOnInit(): void {

  }
}
