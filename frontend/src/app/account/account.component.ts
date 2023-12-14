import { Component, OnInit } from "@angular/core";
import {jwtDecode} from "jwt-decode";
import {HttpClient} from "@angular/common/http";
import {State} from "../../state";
import {ToastController} from "@ionic/angular";
import {FormBuilder} from "@angular/forms";

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

  id: any;
  role: any;
  name: any;
  email: any;

  ngOnInit(): void {
    this.getUser()
  }

  constructor(public http: HttpClient, public state: State, public toastController: ToastController, public fb: FormBuilder) {
  }

  public getUser() {
    const token: any = sessionStorage.getItem("token");

    const decodedToken = jwtDecode(token);
    // @ts-ignore
    this.id = decodedToken["Id"]!;
    // @ts-ignore
    this.name = decodedToken["Name"]!;
    // @ts-ignore
    this.email = decodedToken["Email"]!;
    // @ts-ignore
    this.role = decodedToken["IsAdmin"]!;
  }

  async UpdateUser() {
    try {
      //const observable = this.http.put<ResponseDto<User>>(environment.baseUrl + '/api/account/update/' + this.id, this.updateUserForm.value)
      //const response = await firstValueFrom<ResponseDto<User>>(observable)
      this.ngOnInit()
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
