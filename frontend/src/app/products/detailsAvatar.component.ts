import {Component, OnInit} from "@angular/core";
import {Avatar} from "./products.service";
import {DataService} from "../data.service";

@Component({
  template: `
    <ion-item>
    <ion-label>
      <h1>Avatar Id: {{avatar?.avatar_id}}</h1>
    </ion-label>
    </ion-item>

    <ion-item>
      <ion-label>
        <h2>{{avatar?.avatar_name}}</h2>
      </ion-label>
    </ion-item>

    <ion-item>
      <ion-label>
        <h2>{{avatar?.avatar_price}} €</h2>
      </ion-label>
    </ion-item>

    <ion-item>
      <ion-label>
        <ion-text class="ion-text-wrap">{{avatar?.information}}</ion-text>
      </ion-label>
    </ion-item>

  `
})
export class DetailsAvatarComponent implements OnInit{
  avatar: Avatar | undefined;


  constructor(private data: DataService) {
  }

  ngOnInit() {
    this.data.currentNumber.subscribe(avatar => this.avatar = avatar)
  }
}
