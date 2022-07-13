import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product.models';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  public products: Product[] = [];

  constructor(private productService: ProductService) { 
    this.productService.getProducts()
      .subscribe(products => {
        this.products = products;
      });
  }

  ngOnInit(): void {
  }

}
