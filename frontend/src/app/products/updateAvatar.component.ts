import {Component, OnInit} from "@angular/core";
import {Avatar} from "./products.service";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {DataService} from "../data.service";
import {firstValueFrom} from "rxjs";
import {State} from "../../state";
import {HttpClient} from "@angular/common/http";
import {ModalController, ToastController} from "@ionic/angular";
import {environment} from "../../environments/environment";

@Component({
  template: `
  <ion-list>
    <ion-item>
      <ion-input [formControl]="updateAvatarForm.controls.avatar_name" label="Insert title for avatar please">
        <div *ngIf="!updateAvatarForm.controls.avatar_name.valid">Avatar name must be 3 characters long</div>
      </ion-input>
    </ion-item>

    <ion-item>
      <ion-input [formControl]="updateAvatarForm.controls.avatar_price" type="number" label="Insert price for avatar please">
        <div *ngIf="!updateAvatarForm.controls.avatar_price.valid">Avatar price must be above 1</div>
      </ion-input>
    </ion-item>

    <ion-item>
      <ion-input [formControl]="updateAvatarForm.controls.information" label="Insert information for avatar">
      </ion-input>
    </ion-item>
  </ion-list>

  <ion-button [disabled]="updateAvatarForm.invalid" (click)="submitUpdate()">Change this avatar</ion-button>
  `
})

export class UpdateAvatarComponent implements OnInit {
  avatarElement: Avatar | undefined;
  private router: any;

  constructor(public fb: FormBuilder, private data: DataService, public state: State, public http: HttpClient, public toastController: ToastController, public modalController: ModalController) {
  }

  ngOnInit() {
    this.data.currentNumber.subscribe(avatarElement => this.avatarElement = avatarElement)
  }

  avatar_name = new FormControl(this.data.avatar?.avatar_name, [Validators.minLength(3), Validators.required])
  avatar_price = new FormControl(this.data.avatar?.avatar_price, [Validators.min(1), Validators.required])
  information = new FormControl(this.data.avatar?.information)

  updateAvatarForm = this.fb.group({
    avatar_name: this.avatar_name,
    avatar_price: this.avatar_price,
    information: this.information
  })

  async submitUpdate() {
    let avatarNumber = this.avatarElement?.avatar_id

    try {
      const observable = this.http.put<Avatar>(environment.baseUrl + '/avatar/' + avatarNumber, this.updateAvatarForm.value)
      const response = await firstValueFrom(observable)
      const id = this.state.avatar.findIndex(a => a.avatar_id == response.avatar_id);
      this.state.avatar[id] = response;

      const toast = await this.toastController.create({
        message: 'The avatar was successfully changed',
        duration: 1233,
        color: "success",

      })
      toast.present();

      this.modalController.dismiss();
    } catch (e) {
      const toast = await this.toastController.create({
        message: 'The avatar was unsuccessfully changed',
        duration: 1233,
        color: "danger"

      })
      toast.present();

    }
  }


}
