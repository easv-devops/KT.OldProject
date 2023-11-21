import { Component, OnInit } from "@angular/core";

@Component({
  template: `
    <app-title [title]="'Home'"></app-title>
  `
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
}
