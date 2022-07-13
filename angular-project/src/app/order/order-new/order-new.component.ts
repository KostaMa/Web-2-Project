import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Order } from 'src/app/models/order.models';
import { Product } from 'src/app/models/product.models';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-order-new',
  templateUrl: './order-new.component.html',
  styleUrls: ['./order-new.component.css']
})
export class OrderNewComponent implements OnInit {
  products: Product[] = [];
  selectedProducts: Product[] = [];
  sum: number = 0;
  orders: Order[];
  isAccept: boolean = false;

  public orderForm: FormGroup;

  constructor(private router: Router,
              private formBuilder: FormBuilder, 
              private orderService: OrderService, 
              private productService: ProductService,
              private toastr: ToastrService
              ) { 
    this.orderForm = this.formBuilder.group({
      address: ['', [Validators.required]],
      comment: ['']
    });
  }

  ngOnInit(): void {
    this.loadProducts();
    this.checkForDisable();
  }

  loadProducts() {
    this.productService.getProducts()
    .subscribe(result => {
      this.products = result;
    });
  }

  onSelect(id: number) {
    this.products.forEach(element => {
      if(element.id === id)
      {
        this.selectedProducts.push(element);
        this.sum += element.price;
      }
    });
  }

  onRemove(index: number) {
    for (let i = 0; i < this.selectedProducts.length; i++) {
      if(index == i) {
        this.sum -= this.selectedProducts[index].price;
        this.selectedProducts.splice(index, 1);
      }
    }
  }

  private checkForDisable() {
    this.orderService.getOrdersHistory(localStorage.getItem('personId'))
      .subscribe((result) => {
        result.forEach(e => {
          if(e.orderStatus === 'Accept' || e.orderStatus === 'OnHold') {
            this.isAccept = true;
            this.orderForm.reset();
            this.sum = 0;
            this.selectedProducts = [];
            this.router.navigateByUrl('/home/new-order');
          } else {
            this.orders = result;
            this.isAccept = false;
          }
        });
      });
  }

  public submitForm(data): void {
    if(!this.orderForm.valid) {
      window.alert('Not valid!');
      return;
    }

    this.checkForDisable();
    
    if(this.isAccept === false) {
      let order = new Order(
        0,
        Number(localStorage.getItem('personId')),
        0,
        data.address,
        data.comment,
        'OnHold',
        this.sum + 300,
        this.selectedProducts
      );
  
      this.orderService
        .addOrder(order)
        .subscribe((order: Order) => {
          window.alert('Successfully added an order!');
          this.orderForm.reset();
          this.sum = 0;
          this.selectedProducts = [];
          this.isAccept = true;
        });
    } else {
      this.toastr.error('You can not order new order, when previos is not done.', 'Order error');
    }
    
  }

  get address() {
    return this.orderForm.get('address');
  }

}
