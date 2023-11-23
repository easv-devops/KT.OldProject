import { Component, OnInit } from "@angular/core";
import {ProductsService, Avatar } from './products.service';
import {Observable} from "rxjs";
import {Router} from "@angular/router";
@Component({
  template: `
    <app-title title="Avatars"></app-title>
    <ion-content>
      <ion-grid [fixed]="true">
        <ion-row>
          <ion-col *ngFor="let avatar of avatar$ | async; index as i">
            <ion-card>
              <ion-card-header>
                <ion-card-title>{{avatar.id}}</ion-card-title>
              </ion-card-header>
              <ion-card-content>
                {{avatar.name}}
              </ion-card-content>
            </ion-card>
          </ion-col>
        </ion-row>
      </ion-grid>
    </ion-content>

  `
})

//<img src="https://robohash.org/abc.png"/>

export class ProductsComponent implements OnInit {


  avatar$?: Observable<Avatar[]>;

  constructor(private service: ProductsService, readonly router: Router) {
  }

  ngOnInit(): void {

    this.avatar$ = this.service.getAllProducts();


  }

}
