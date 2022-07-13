import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../models/order.models';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.css']
})
export class CountdownComponent implements OnInit {
  minutes:number=0;
  seconds:number=0;
  order:Order | null;
  interval:any; 
  finished: boolean=true;
  constructor(private orderService: OrderService, private router: Router) {
    let time;
    if(localStorage.getItem('role') === 'Deliverer') {
      this.orderService.getMyOrders(localStorage.getItem('personId'))
        .subscribe((result) => {
          if(result.length !== 0) {
            let oneOrder = result[result.length - 1];
            if(oneOrder.orderStatus === 'Done') {
              this.order = null;
            } else {
              this.order = result[result.length - 1];
              this.finished=false;
              this.order=result[result.length - 1];  
              let now:Date=new Date();
              time = Date.now() - +(new Date(result[result.length - 1].dateTimeOfDelivery));
              time =- time / 1000;
              this.minutes = Math.floor(time / 60);
              this.seconds = Math.floor(time - this.minutes * 60);
              this.interval = setInterval(()=>{
                if(this.seconds > 0)
                {
                  this.seconds = this.seconds - 1;
                }
                else
                {
                  if(this.minutes === 0)
                  {
                    this.StopTimer1();
                  }
                  this.seconds = 59;
                  this.minutes = this.minutes - 1;
                }
              },1000);
            }
          } else {
            this.order = null;
          }
        }
      );
    } else if(localStorage.getItem('role') === 'Customer') {
      this.orderService.getCurrentOrder(localStorage.getItem('personId')).subscribe(
        (data : Order | null) => {
          if(data !== null && data.orderStatus !== 'Done')
          {
            this.finished=false;
            this.order=data;  
            let now:Date=new Date();
            console.log(time + ' 1');
            time = Date.now() - +(new Date(data.dateTimeOfDelivery));
            console.log(time + ' 2');
            time =- time / 1000;
            console.log(time + ' 3');
            this.minutes = Math.floor(time / 60);
            console.log(this.minutes + ' 4');
            this.seconds = Math.floor(time - this.minutes * 60);
            this.interval = setInterval(()=>{
              if(this.minutes < 0) {
                this.StopTimer2();
              }
              if(this.seconds > 0)
              {
                this.seconds = this.seconds - 1;
              }
              else
              {
                if(this.minutes <= 0)
                {
                  this.StopTimer2();
                }
                this.seconds = 59;
                this.minutes = this.minutes - 1;
              }
            },1000);
          } 
      });
    }
  }

  StopTimer1() { 
    this.orderService.confiramtion(this.order?.id).subscribe(() => { });
    clearInterval(this.interval);
    this.finished=true;
  }

  StopTimer2() { 
    clearInterval(this.interval);
    this.finished=true;
  }

  ngOnInit(): void {
  }

}
