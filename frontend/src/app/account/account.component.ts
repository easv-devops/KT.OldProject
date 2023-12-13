import { Component, OnInit } from "@angular/core";
import {jwtDecode} from "jwt-decode";

@Component({
  template: `
    <app-title title="Account"></app-title>
    <ion-content>
      <form>
        <ion-list class="field-list">
          <ion-item>
            <ion-input label="Name" [value]="this.name"></ion-input>
          </ion-item>
          <ion-item>
            <ion-input label="Email" [value]="this.email"></ion-input>
          </ion-item>
          <ion-item>
            <ion-input label="Admin" [value]="this.role"></ion-input>
          </ion-item>
        </ion-list>
        <ion-button>Update</ion-button>
      </form>
    </ion-content>
  `,
  styleUrls: ['./form.css'],
})
export class AccountComponent implements OnInit {

  role: any;
  name: any;
  email: any;
  ngOnInit(): void {
    this.getUser()
  }
  public getUser(){
    const token: any = sessionStorage.getItem("token");

    const decodedToken  = jwtDecode(token);
    // @ts-ignore
    this.name = decodedToken["Name"]!;
    // @ts-ignore
    this.email = decodedToken["Email"]!;
    // @ts-ignore
    this.role = decodedToken["IsAdmin"]!;
  }
}
