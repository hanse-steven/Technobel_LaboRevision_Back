﻿DROP TABLE IF EXISTS CartItem;
CREATE TABLE CartItem
(
    Id          INT     PRIMARY KEY GENERATED BY DEFAULT AS IDENTITY,
    Session     UUID    NOT NULL,
    Product     UUID    NOT NULL,
    Quantity    INT     NOT NULL CHECK (Quantity > 0),
    CONSTRAINT u_cartitem UNIQUE (Session, Product)
);
INSERT INTO CartItem (Session, Product, Quantity) VALUES
('816da7cc-d469-41ae-a539-7b4f5b18ade1', '7406b2cc-2c22-4157-9fba-d56a969bde7c', 2);

DROP TABLE IF EXISTS Product;
CREATE TABLE Product
(
    Id          UUID            PRIMARY KEY,
    Name        VARCHAR(50)     NOT NULL,
    Price       DECIMAL(18, 2)  NOT NULL CHECK (Price > 0),
    Quantity    INT             NOT NULL CHECK (Quantity >= 0)
);

INSERT INTO Product VALUES
('7406b2cc-2c22-4157-9fba-d56a969bde7c', 'Pizza', 5, 7),
('eae4e338-a230-46ae-b29b-357053b7627e', 'Burger', 5, 7),
('2d764f53-9371-4bbd-b9f3-31e85dec8a6d', 'Pâte', 8, 6),
('8d9164eb-99c9-421d-b603-7c2c95ff8dae', 'Salade', 4, 5),
('e769faad-6a86-44cc-a87f-78efb9a5a6e8', 'Sushi', 12, 4),
('16de1a34-e5cb-4b5a-9197-5be3b4eaeee6', 'Tacos', 6, 3),
('a8896983-47ec-4daf-a00f-d174d43e10a2', 'Frite', 3, 2),
('6b7ea087-e29b-4cbd-adba-86113c232ef7', 'Glace', 2, 1),
('fe1f7583-5d30-413e-8f7c-230368470af3', 'Donuts', 1, 0),
('2d935bff-c81d-44d6-8f39-8d154d389ac7', 'Cupcake', 1, 9);






