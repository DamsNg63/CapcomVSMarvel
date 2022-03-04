import {Component, Input, OnInit} from '@angular/core';
import { BoardDto, GameDto, GameServiceProxy } from 'src/shared/service-proxy/service-proxy';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {interval} from 'rxjs';
import {takeWhile} from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-power4',
  templateUrl: './power4.component.html',
  styleUrls: ['./power4.component.css']
})
export class Power4Component implements OnInit {
  gameId!: string;
  playerId!: string;
  api: GameServiceProxy;
  board: BoardDto;
  canPlay = false;
  win = false;
  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {
    this.api = new GameServiceProxy(http, environment.baseUrl)
    this.board = new BoardDto()
    this.board.board = []
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.gameId = params['gameId'];
      this.playerId = params['playerId'];
    })
    this.updateBoard();
    interval(1000)
        .pipe(takeWhile(() => !this.canPlay))
        .subscribe(() => {
          this.checkCanPlay()
        });
  }

  range(start: number, end: number, step: number) {
    const foo = [];
    if(step > 0){
      for (let i = start; i < end; i+=step) {
        foo.push(i);
      }
    }else{
      for (let i = start; i > end; i+=step) {
        foo.push(i);
      }
    }
    return foo;
  }

  play(col: number | undefined){
    this.canPlay = false;
    this.api.play(this.playerId, this.gameId, col).subscribe(data =>{
      if(data===false){
        this.canPlay = true;
      }else{
        this.checkWin();
        this.updateBoard();
        interval(1000)
            .pipe(takeWhile(() => !this.canPlay))
            .subscribe(() => {
              this.checkCanPlay()
            });
      }

    })
  }

  checkCanPlay(){
    this.api.canplay(this.playerId, this.gameId).subscribe(data => {
      if(data === true){
        this.canPlay = true;
        this.updateBoard();
      }
    })
  }

  updateBoard(){
    this.api.board(this.gameId).subscribe(board => this.board = board)
  }

  checkWin(){
    this.api.win(this.gameId).subscribe(data => {
      if(data !== 0){
        this.win = true;
      }
    })
  }

}
