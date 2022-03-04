import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { delay, interval, takeWhile } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BoardDto, GameServiceProxy } from 'src/shared/service-proxy/service-proxy';

@Component({
  selector: 'app-invite-code',
  templateUrl: './invite-code.component.html',
  styleUrls: ['./invite-code.component.css']
})
export class InviteCodeComponent implements OnInit {

  api: GameServiceProxy
  gameId = ''
  playerId = ''
  joinUrl = 'wait'
  showalert = false;
  gameReady = false;

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {
    this.api = new GameServiceProxy(http, environment.baseUrl)
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.gameId = params['gameId'];
      this.joinUrl = environment.hostUrl + '/#/invite?gameId=' + params['gameId']
      this.playerId = params['playerId'];
      interval(2500)
          .pipe(takeWhile(() => !this.gameReady))
          .subscribe(() => {
            this.checkReady()
          });
    })
    if (this.playerId == undefined) {
      this.api.join(this.gameId).subscribe(data => {
        console.log(data.playerID)
        this.router.navigate(['/invite'], {queryParams:{gameId: this.gameId, playerId: data.playerID}})
      })
    }
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

  checkReady(){
    this.api.ready(this.gameId).subscribe(data => {
      if (data === true){
        this.gameReady = true;
        this.router.navigate(['/game'], {queryParams:{gameId: this.gameId, playerId: this.playerId}})
      }
    })
  }

}
