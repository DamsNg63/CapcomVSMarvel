import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs';

@Component({
  selector: 'app-invite-code',
  templateUrl: './invite-code.component.html',
  styleUrls: ['./invite-code.component.css']
})
export class InviteCodeComponent implements OnInit {

  showalert = false;

  constructor() { }

  ngOnInit(): void {
  }

  copyMessage(){
    this.showalert = true;
    console.log(this.showalert);
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = "Hello";
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
    setTimeout(() => this.showalert = false, 7000);
  }
}
