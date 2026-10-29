 ## Database Design (ERD)

The Entity Relationship Diagram (ERD) illustrates the structure of the database for the MAUI Offline Shopping App. It contains six entities: Customer, Category, Product, CartItem, Order and OrderItem.

The diagram shows the attributes, primary keys (PK), foreign keys (FK) and relationships between the entities. It represents how customers select products, add them to their carts and complete purchases.

## ERD Diagram

![MAUI Offline Shopping App ERD](OfflineMobileApp/Documents/erd.png)

## Why Does OrderIem Store Its Own Price?
The OdretItem entity stores its own UnitPrice to preserve the price of a product at the time of purchase.
A products current price may change in the future, but previous orders must still reflect the amount the customer originally paid.

For example, if a customer purchase purchase a hoodie for R330 and the price later icrease to R350, the UnitPrice in the existing OderItem remains R330.
This ensures that order history stay accurate and the original purchase total can be calculated correctly.

The line total is calculated by multiplying the quantity by the unit price recorded at checkout.
## Fornula: Line Total = Quantity * UnitPrice
