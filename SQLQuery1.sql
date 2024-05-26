use master
CREATE DATABASE dapperpath

use dapperpath
CREATE TABLE Users (
  UserID INT IDENTITY(1,1) PRIMARY KEY,
  Username VARCHAR(255) NOT NULL,
  PasswordHash VARCHAR(255) NOT NULL,
  Status bit(1),--true - admin      false- user
  IsConnected bit (1),
  IsBanned bit(1),
  IsPending bit (1) 

);


CREATE TABLE Shoes (
  ProductID INT IDENTITY(1,1) PRIMARY KEY,
  Title VARCHAR(255) NOT NULL,
  Brand VARCHAR(255),
  CategoryID int,
  Price DECIMAL(10, 2),
  AvailableSizes VARCHAR(255),
  Description TEXT,
  Image VARCHAR(255),
  Sex NVARCHAR(1),
  UnavailableSizes VARCHAR(255),
  Sale decimal (10,2)
);
insert into Shoes values('Nike Air Force', 'Nike','300', '40 41 42 43 44 45','какие же крутые', 'D:\Desktop\ekz\wpf_za_7dney\db_image\force.png', 'M',11,'37 38','250')
insert into Shoes values('Adidas Ozweego', 'Adidas','400' ,'40 41 42 43 44 45','что этьо такое',  'D:\Desktop\ekz\wpf_za_7dney\db_image\ozweego.webp', 'M',11,'37 38','300')
insert into Shoes values('Puma Cassia Via', 'Puma', '350','40 41 42 43 44 45','о боже мой', 'D:\Desktop\ekz\wpf_za_7dney\db_image\cassia.webp', 'W',11,'37 38','200')


select * from Shoes;

CREATE TABLE Cart (
  CartID INT IDENTITY(1,1) PRIMARY KEY,
  UserID INT,
  ProductID INT,
  Size varchar(2),
  FOREIGN KEY (UserID) REFERENCES Users(UserID),
  FOREIGN KEY (ProductID) REFERENCES Shoes(ProductID)
);


CREATE TABLE Orders (
  OrderID INT IDENTITY(1,1) PRIMARY KEY,
  UserID INT,
  ProductID INT,
  Size varchar(2),
  ShippingAddress VARCHAR(255),
  ShippingMethod VARCHAR(255),
  PaymentMethod VARCHAR(255),
  Sum DECIMAL (10,2),
  FOREIGN KEY (ProductID) REFERENCES Shoes(ProductID),
  FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE Reviews (
  ReviewID INT IDENTITY(1,1) PRIMARY KEY,
  UserID INT,
  ProductID INT,
  ReviewText TEXT,
  FOREIGN KEY (UserID) REFERENCES Users(UserID),
  FOREIGN KEY (ProductID) REFERENCES Shoes(ProductID)
);

CREATE TABLE Wishlist (
  WishlistID INT IDENTITY(1,1) PRIMARY KEY,
  UserID INT,
  ProductID INT,
  Size VARCHAR(10),
  IsAvailable bit(1),
  FOREIGN KEY (UserID) REFERENCES Users(UserID),
  FOREIGN KEY (ProductID) REFERENCES Shoes(ProductID)
);
CREATE TABLE ShoeCategory (
  CategoryID INT IDENTITY(1,1) PRIMARY KEY,
  CategoryName VARCHAR(255) NOT NULL
);

ALTER TABLE Shoes
ADD CategoryID INT,
    FOREIGN KEY (CategoryID) REFERENCES ShoeCategory(CategoryID);
    INSERT INTO ShoeCategory (CategoryName)
VALUES ('Кроссовки'), ('Ботинки'), ('Спортивная'), ('Туфли');



