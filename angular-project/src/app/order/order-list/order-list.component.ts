import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AcceptOrder } from 'src/app/models/acceptOrder.model';
import { Order } from 'src/app/models/order.models';
import { OrderService } from 'src/app/services/order.service';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  orders: Order[] = [];
  role: string | null;
  idPerson: string | null;
  isDisabled: boolean = false;

  constructor(private orderService: OrderService, 
              private personService: PersonService, 
              private toastr: ToastrService
              ) { }

  ngOnInit(): void {
    this.checkPerson();
  }

  private checkPerson() {
    let role = localStorage.getItem('role');
    if(role === 'Admin') {
      this.orderService.getOrders()
      .subscribe(orders => {
        this.orders = orders;
      });
      this.role = localStorage.getItem('role');
      this.idPerson = localStorage.getItem('idPerson');
    }
    if(role === 'Deliverer') {
      this.orderService.getOrders()
        .subscribe(orders => {
          orders.forEach(e => {
            if(e.orderStatus === 'Accept') {
              this.isDisabled = true;
            } else {
              this.isDisabled = false;
            }
          });
          this.orders = orders;
        });
      this.role = localStorage.getItem('role');
      this.idPerson = localStorage.getItem('idPerson');
    }
  }

  acceptOrder(idOrder: number) {
    this.orderService.getOrders()
      .subscribe(orders => {
        orders.forEach(e => {
          if(e.orderStatus === 'Accept') {
            this.isDisabled = true;
          } else {
            this.isDisabled = false;
          }
        });
      });
    
    if(this.isDisabled === false) {
      let orderAccept = new AcceptOrder(idOrder, Number(localStorage.getItem('personId')));
      this.orderService.acceptOrder(orderAccept)
          .subscribe(() => {
            alert('Successfully added an order!');
            this.orderService.getOrders()
            .subscribe(orders => {
              this.orders = orders;
              this.isDisabled = true;
            });
          });
      this.isDisabled = true;
    } else {
      this.toastr.error('You can not order new order, when previos is not done.', 'Order error');
    }
  }

}
