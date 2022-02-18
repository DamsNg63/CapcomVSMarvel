import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { GameDto, GameServiceProxy } from 'src/shared/service-proxy/service-proxy';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  api: GameServiceProxy;

  constructor(private http: HttpClient, private router: Router) {
    this.api = new GameServiceProxy(http, environment.baseUrl);
  }

  ngOnInit(): void {
  }

  newGame(){
    this.api.create().subscribe((data: GameDto) => {
      this.router.navigate(['/invite'], {queryParams:{gameId: data.gameId, playerId: data.playerID}})
    })
  }

}
