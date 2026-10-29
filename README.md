#Why Does OrderIem Store Its Own Price?
The OdretItem entity stores its own UnitPrice to preserve the price of a product at the time of purchase.
A products current price may change in the future, but previous orders must still reflect the amount the customer originally paid.

For example, if a customer purchase purchase a hoodie for R330 and the price later icrease to R350, the UnitPrice in the existing OderItem remains R330.
This ensures that order history stay accurate and the original purchase total can be calculated correctly.

The line total is calculated by multiplying the quantity by the unit price recorded at checkout.
#Fornula: Line Total = Quantity * UnitPrice
