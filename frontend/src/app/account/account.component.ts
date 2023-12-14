import { Component, OnInit } from "@angular/core";
import {jwtDecode} from "jwt-decode";
import {HttpClient} from "@angular/common/http";
import {State} from "../../state";
import {ToastController} from "@ionic/angular";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {ResponseDto} from "./account.service";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";
import {TokenResponse} from "./login.component";
import {TokenService} from "../../services/token.service";

@Component({
  template: `
    <app-title title="Account"></app-title>
    <ion-content>
      <form>
        <ion-list class="field-list">
          <ion-item>
            <ion-input [formControl]="updateUserForm.controls.full_name" label="Name" ></ion-input>
          </ion-item>
          <ion-item>
            <ion-input [formControl]="updateUserForm.controls.email" label="Email" ></ion-input>
          </ion-item>
        </ion-list>
        <ion-button (click)="UpdateUser()">Update</ion-button>
      </form>
    </ion-content>
  `,
  styleUrls: ['./form.css'],
})
export class AccountComponent implements OnInit {

  token: any = sessionStorage.getItem("token");
  decodedToken: any = jwtDecode(this.token);
  user_id: any;

  ngOnInit(): void {
    this.user_id = this.decodedToken["Id"]
  }

  constructor(public http: HttpClient, public state: State, public toastController: ToastController, public fb: FormBuilder, public newToken: TokenService) {
  }
  // @ts-ignore
  full_name = new FormControl(this.decodedToken["Name"], Validators.required)
  // @ts-ignore
  email = new FormControl(this.decodedToken["Email"], Validators.required)

  updateUserForm = this.fb.group({
    full_name: this.full_name,
    email: this.email
  })

  async UpdateUser() {
    try {
     var response = await firstValueFrom(this.http.put<ResponseDto<TokenResponse>>(environment.baseUrl + '/api/account/update/' + this.user_id, this.updateUserForm.value))
      this.newToken.setToken(response.responseData?.token!);
      const toast = await this.toastController.create({
        message: 'The user was successfully changed',
        duration: 1233,
        color: "success",
      })
      toast.present();
    } catch (e) {
      const toast = await this.toastController.create({
        message: 'The user was unsuccessfully changed',
        duration: 1233,
        color: "danger"

      })
      toast.present();
    }
  }
}
