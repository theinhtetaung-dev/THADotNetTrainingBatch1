DROP DATABASE IF EXISTS StudentDb;

CREATE DATABASE StudentDb;
USE StudentDb;

CREATE TABLE Tbl_Students (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Major VARCHAR(100)
);

INSERT INTO Tbl_Students (Name, Major) VALUES ('Kyaw Kyaw', 'CS');
INSERT INTO Tbl_Students (Name, Major) VALUES ('Su Su', 'CT');