import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../models/product.models';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private products: Observable<Product[]>;

  constructor(private http: HttpClient, router: Router) { 
    this.refreshProducts();
  }
  
  private refreshProducts(): Observable<Product[]> {
    this.products = this.http
      .get<Product[]>(environment.otherServiceUrl + '/api/product');

    return this.products;
  }

  public getProducts() {
    return this.products;
  }

  public addProduct(data) {
    data.name = data.prodName;
    return this.http
      .post(environment.otherServiceUrl + '/api/product', data);
  }


}
