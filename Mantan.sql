CREATE DATABASE IF NOT EXISTS daftar_mantan_db;
USE daftar_mantan_db;

CREATE TABLE IF NOT EXISTS mantan (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    tanggal_lahir DATE NOT NULL,
    ciri_ciri TEXT,
    foto_path VARCHAR(255)
);
