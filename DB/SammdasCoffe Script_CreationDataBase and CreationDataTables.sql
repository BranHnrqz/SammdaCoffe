Create Database SammdasCoffee
use SammdasCoffee

-- Tabla Category
CREATE TABLE Category (
  categoryID INT PRIMARY KEY identity(1,1),
  categoryName VARCHAR(255)
);

-- Tabla Product
CREATE TABLE Product (
  productID INT PRIMARY KEY identity(1,1),
  categoryID INT,
  productName VARCHAR(255),
  FOREIGN KEY (categoryID) REFERENCES Category(categoryID)
);

-- Tabla Ingredient
CREATE TABLE Ingredient (
  ingredientID INT PRIMARY KEY identity(1,1),
  ingredientName VARCHAR(255)
);

-- Tabla ProductDetail
CREATE TABLE ProductDetail (
  detailID INT PRIMARY KEY,
  productID INT,
  ingredientID INT,
  size VARCHAR(50),
  productPrice DECIMAL(10, 2),
  descriptionProduct VARCHAR(465),
  FOREIGN KEY (productID) REFERENCES Product(productID),
  FOREIGN KEY (ingredientID) REFERENCES Ingredient(ingredientID)
);


-- Tabla Rol
CREATE TABLE Rol (
  rolID INT PRIMARY KEY identity(1,1),
  rolName VARCHAR(255)
);

-- Tabla User
CREATE TABLE UserType (
  userID INT PRIMARY KEY identity(1,1),
  userName VARCHAR(255),
  userMail VARCHAR(255),
  userPassword VARCHAR(255),
  rolID INT,
  FOREIGN KEY (rolID) REFERENCES Rol(rolID)
);

