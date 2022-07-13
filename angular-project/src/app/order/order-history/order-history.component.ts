import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order.models';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent implements OnInit {
  orders: Order[] = [];

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadHistoryOrders()
  }

  private loadHistoryOrders() {
    const id = localStorage.getItem('personId');
    this.orderService.getOrdersHistory(id)
      .subscribe(orders => {
        this.orders = orders;
      });
  }

}
