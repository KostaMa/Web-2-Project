import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AcceptOrder } from '../models/acceptOrder.model';
import { Order } from '../models/order.models';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private orders: Observable<Order[]>;

  constructor(private http: HttpClient, router: Router) { 
    this.refreshOrders();
  }

  private refreshOrders(): Observable<Order[]> {
    this.orders = this.http
      .get<Order[]>(environment.otherServiceUrl + '/api/order');

    return this.orders;
  }

  getOrders() {
    return this.orders;
  }
  
  confiramtion(idOrder: number | null | undefined) {
    return this.http.get(environment.otherServiceUrl + '/api/order/confirmation/' + idOrder);
  }

  getCompletedOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(environment.otherServiceUrl + '/api/order/complated-orders');
  }

  getMyOrders(id: string | null):  Observable<Order[]> {
    return this.http.get<Order[]>(environment.otherServiceUrl + '/api/order/' + id + '/my-orders')
  }

  getOrdersHistory(id: string | null): Observable<Order[]> {
    return this.http.get<Order[]>(environment.otherServiceUrl + '/api/order/' + id + '/orders')
  }

  getCurrentOrder(id: string | null): Observable<Order> {
    return this.http.get<Order>(environment.otherServiceUrl + '/api/order/' + id + '/current')
  }

  addOrder(order: Order) {
    return this.http
    .post<Order>(environment.otherServiceUrl + '/api/order/order', order);
  }

  public acceptOrder(data) {
    return this.http
      .post<AcceptOrder>(environment.otherServiceUrl + '/api/order/accept-order', data);
  }

}
