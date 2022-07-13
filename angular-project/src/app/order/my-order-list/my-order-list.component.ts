import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order.models';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-my-order-list',
  templateUrl: './my-order-list.component.html',
  styleUrls: ['./my-order-list.component.css']
})
export class MyOrderListComponent implements OnInit {
  orders: Order[];

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadMyOredrs();
  }

  private loadMyOredrs() {
    const id = localStorage.getItem('personId');
    this.orderService.getMyOrders(id)
      .subscribe(orders => {
        this.orders = orders;
      });
  }

}
