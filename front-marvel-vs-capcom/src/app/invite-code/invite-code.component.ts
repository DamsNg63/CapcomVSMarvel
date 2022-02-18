import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { delay, interval, takeWhile } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ServiceProxy } from 'src/shared/service-proxy/service-proxy';

@Component({
  selector: 'app-invite-code',
  templateUrl: './invite-code.component.html',
  styleUrls: ['./invite-code.component.css']
})
export class InviteCodeComponent implements OnInit {

  api: ServiceProxy
  gameId = ''
  playerId = ''
  joinUrl = 'wait'
  showalert = false;

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {
    this.api = new ServiceProxy(http, environment.baseUrl)
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.gameId = params['gameId'];
      this.joinUrl = environment.hostUrl + '/#/invite?gameId=' + params['gameId']
      this.playerId = params['playerId']
    })
  }



  copyMessage(){
    this.showalert = true;
    console.log(this.showalert);
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = this.joinUrl;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
    setTimeout(() => this.showalert = false, 7000);
  }
}
