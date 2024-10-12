import { Component } from "@angular/core";
import { RouterExtensions } from "@nativescript/angular";

@Component({
  selector: "Home",
  templateUrl: "./home.component.html"
})
export class HomeComponent {
  constructor(private routerExtensions: RouterExtensions) {}

  onStartGame() {
    this.routerExtensions.navigate(["/game"]);
  }
}