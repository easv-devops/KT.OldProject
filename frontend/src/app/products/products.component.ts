import {Component, EventEmitter, OnInit} from "@angular/core";
import {ProductsService, Avatar} from './products.service';
import {Router} from "@angular/router";

@Component({
  template: `
    <app-title title="AVATARS"></app-title>

    <ion-content>
      <app-search (searchTextChanged)="onSearchTextEntered($event)" style="position: absolute; top: 0;"></app-search>



      <ion-grid [fixed]="true">
        <ion-row>
          <ion-col *ngFor="let avatar of avatar$; index as i">
            <ion-card *ngIf="searchText === '' || avatar.avatar_name.toLowerCase().includes(searchText)">
              <ion-card-header>
                <ion-card-title class="name">{{avatar.avatar_name}}</ion-card-title>
                <ion-card-title class="price">{{avatar.avatar_price}} € </ion-card-title>
              </ion-card-header>
              <img src="https://robohash.org/{{avatar.avatar_name}}.png" height="150px" width="150px"/>
              <ion-card-content>
                <ion-button class="button" (click)="saveData(avatar)" fill="clear" >
                  <ion-icon name="cart-outline"></ion-icon>
                </ion-button>
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
  cartArray: Avatar[];


  constructor(private productService: ProductsService, readonly router: Router) {
    this.cartArray = [];
}

  searchText: string = '';

  onSearchTextEntered(searchValue: string) {
    this.searchText = searchValue;
    console.log(this.searchText)
  }



  saveData(avatar: Avatar){
    this.cartArray.push(avatar);
    sessionStorage.setItem("cart", JSON.stringify(this.cartArray))
  }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(result => {
      this.avatar$ = result.responseData;
    })
  }
}
