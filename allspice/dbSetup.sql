CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS recipes(
  id INT AUTO_INCREMENT PRIMARY KEY,
  creatorId varchar(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title TEXT NOT NULL,
  subtitle TEXT,
  category TEXT NOT NULL,
  picture VARCHAR(255),
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS ingredients(
  id INT AUTO_INCREMENT PRIMARY KEY,
  recipeId INT NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name TEXT NOT NULL,
  quantity TEXT NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS steps(
  id INT AUTO_INCREMENT PRIMARY KEY,
  recipeId INT NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  place INT NOT NULL,
  body TEXT NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
INSERT INTO
  recipes (title, subtitle, category, picture, creatorId)
VALUES
  (
    'Home Fries',
    'Best damn home fries around!',
    'fries',
    'https://www.jessicagavin.com/wp-content/uploads/2021/04/home-fries-17-1200-500x500.jpg',
    '624f5987f54ef8f911d132ad'
  );
SELECT
  *
FROM
  recipes;
DELETE FROM
  recipes
WHERE
  id = 2
LIMIT
  1;
INSERT INTO
  ingredients (name, quantity, recipeId)
VALUES
  ("pepper", "2 tsp", 3);
SELECT
  *
FROM
  ingredients;
DELETE FROM
  ingredients;
SELECT
  *
FROM
  ingredients
WHERE
  recipeId = 3;
DROP TABLE recipes;
UPDATE
  ingredients
SET
  name = "Ham",
  quantity = "4 slices"
WHERE
  id = 8;