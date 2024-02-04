CREATE DATABASE felvetelizok DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;



CREATE TABLE adatok (
  OMAzonosito varchar(11) COLLATE utf8_hungarian_ci NOT NULL,
  Nev varchar(45) COLLATE utf8_hungarian_ci NOT NULL,
  ErtesitesiCim varchar(120) COLLATE utf8_hungarian_ci NOT NULL,
  SzuletesiDatum date NOT NULL,
  ElerhetosegEmail varchar(40) COLLATE utf8_hungarian_ci NOT NULL,
  MatekPontszam int(11) DEFAULT NULL,
  MagyarPontszam int(11) DEFAULT NULL
  )