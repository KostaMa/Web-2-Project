import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-new',
  templateUrl: './product-new.component.html',
  styleUrls: ['./product-new.component.css']
})
export class ProductNewComponent implements OnInit {

  public productForm: FormGroup;

  constructor(private productService: ProductService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService          
    ) { 
    this.productForm = this.formBuilder.group({
      prodName: ['', Validators.required],
      price: ['', Validators.required],
      ingredients: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }
  
  onSubmit(data) {
    if(this.productForm.valid) {
      this.productService.addProduct(data)
        .subscribe(() => {
          alert('Successfully added a product!');
          this.productForm.reset();
        })
    }
  }

  get prodName() {
    return this.productForm.get('prodName');
  }

  get price() {
    return this.productForm.get('price');
  }

  get ingredients() {
    return this.productForm.get('ingredients');
  }

}
