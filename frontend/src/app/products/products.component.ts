import {Component, EventEmitter, OnInit} from "@angular/core";
import {ProductsService, Avatar} from './products.service';
import {Router} from "@angular/router";
import {createAvatarComponent} from "./createAvatar.component";
import {ModalController} from "@ionic/angular";

@Component({
  template: `
    <app-title title="AVATARS"></app-title>

    <ion-content color="light">

        <app-search (searchTextChanged)="onSearchTextEntered($event)" style="position: absolute; top: 0;"></app-search>



      <br>

      <ion-list inset="true" *ngFor="let avatar of avatar$; index as i">
        <ion-item *ngIf="searchText === '' || avatar.avatar_name.toLowerCase().includes(searchText)">
          <img src="https://robohash.org/{{avatar.avatar_name}}.png" height="150px" width="150px"/>
          <ion-label>{{avatar.avatar_name}}</ion-label>
          <ion-label>{{avatar.avatar_price}} €</ion-label>

          <ion-button class="button" (click)="saveData(avatar)" fill="clear" >
            <ion-icon name="cart-outline"></ion-icon>
          </ion-button>

        </ion-item>
      </ion-list>
      <ion-button class="button" (click)="createAvatar()">Create</ion-button>
    </ion-content>
  `
})

export class ProductsComponent implements OnInit {


  avatar$?: Avatar[];
  cartArray: Avatar[];


  constructor(private productService: ProductsService, readonly router: Router, public modalController: ModalController) {
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

  async createAvatar(){
    const model = await this.modalController.create({
      component: createAvatarComponent
    })
    model.present();
  }
}
