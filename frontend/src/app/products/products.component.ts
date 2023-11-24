import { Component, OnInit } from "@angular/core";
import {ProductsService, Avatar, ResponseDto} from './products.service';
import {Router} from "@angular/router";
@Component({
  template: `
    <app-title title="Avatars"></app-title>
    <ion-content>
      <ion-grid [fixed]="true">
        <ion-row>
          <ion-col *ngFor="let avatar of avatar$; index as i">
            <ion-card>
              <ion-card-header>
                <ion-card-title>{{avatar.avatar_name}}</ion-card-title>
                <ion-card-title>{{avatar.avatar_price}} € </ion-card-title>
              </ion-card-header>
              <img src="https://robohash.org/{{avatar.avatar_name}}.png" height="150px" width="150px"/>
              <ion-card-content>
                <ion-button fill="clear" >
                  <ion-icon name="cart-outline"></ion-icon>
                </ion-button>
                <button (click)="saveData()">Save Data in Session Storage</button>
              </ion-card-content>
            </ion-card>
          </ion-col>
        </ion-row>
      </ion-grid>
    </ion-content>
  `
})

export class ProductsComponent implements OnInit {


  avatar$?: Avatar[];

  constructor(private service: ProductsService, readonly router: Router) {
  }

  saveData(){

    sessionStorage.setItem('name', 'Rana Hasnain');

  }

  ngOnInit(): void {

    this.service.getAllProducts().subscribe(result => {
      this.avatar$ = result.responseData;
    })
  }

}
