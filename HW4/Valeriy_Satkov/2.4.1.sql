﻿USE Northwind;
SELECT DISTINCT	
   (SELECT suppls.CompanyName 
    FROM Suppliers AS suppls 
	WHERE suppls.SupplierID IN (prducts.SupplierID)) AS 'Suppliers without product'
FROM Products AS prducts
WHERE UnitsInStock = 0;
