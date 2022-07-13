import { Product } from "./product.models";

export class Order {
    constructor(public id: number,
                public idCustomer: number,
                public idDeliverer: number,
                public address: string,
                public comment: string,
                public orderStatus: string,
                public total: number,
                public products: Product[],
                public dateTimeOfDelivery: Date = new Date()) {

    }
    
}