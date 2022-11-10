using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop;



    /*
     Describe with UML ‘internet shop’ domain. It should include products (with price, quantity, etc.), buyers (personal information), receipts (what was sold and to whom), etc.

    Implement all entities in C# program

    Provide console interface to register new product, add quantity to existent, sell product, register buyer.
     */

    string customerFile = "Customers.csv";
    string productFile = "Products.csv";


Customer.OperationWithCustomers(customerFile); // Implemented the following operations: Create cusomer, Remove cusomer, GetCustomerById, PrintCustomerList 

Products.OperationWithProducts(productFile); // Implemented the following operations: Create product, Remove product, GetProductById, PrintProductList and total quantity. 

Order.MakeOrder(productFile); // Implemented the following operations: Create order, Print receipt on screen.

