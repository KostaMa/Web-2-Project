import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Order } from 'src/app/models/order.models';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-current',
  templateUrl: './order-current.component.html',
  styleUrls: ['./order-current.component.css']
})
export class OrderCurrentComponent implements OnInit {
  @Input() order: Order | null;

  constructor(private orderService: OrderService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadOrder();
  }

  private loadOrder() {
    const role = localStorage.getItem('role');
    const id = localStorage.getItem('personId');
    if(role === 'Deliverer') {
      this.orderService.getMyOrders(id)
        .subscribe((result) => {
          if(result.length !== 0) {
            let oneOrder = result[result.length - 1];
            if(oneOrder.orderStatus === 'Done') {
              this.order = null;
            } else {
              this.order = result[result.length - 1];
            }
          } else {
            this.order = null;
          }
        },
        error => {
          this.toastr.error(error.error.message, 'Order failed.');
        }
        );
    } else if(role === 'Customer') {
      this.orderService.getCurrentOrder(id)
        .subscribe((result) => {
          if(result.orderStatus === 'Done') {
            this.order = null;
          } else {
            this.order = result;
          }
        },
        error => {
            this.toastr.error(error.error.message, 'Order failed.');
        }
        );
    }

  }

}
