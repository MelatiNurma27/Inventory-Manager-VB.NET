# Inventory Manager VB.NET

A desktop-based inventory management application built with Visual Basic .NET in Visual Studio 2022. This app connects to a MySQL database using ODBC via XAMPP and implements a disconnected environment using `DataSet` for local data manipulation.

## Features

- Add, update, delete product data locally via DataSet
- Sync changes to MySQL database with a "Save" button
- View data in real-time through a DataGridView
- Disconnected architecture: changes are only saved permanently on demand

## How to Run the App

1. Download this repository as a ZIP file and extract it to your desired location.
2. Open XAMPP and start both Apache and MySQL modules.
3. Open phpMyAdmin, then:
   - Create a new database
   - Import the sql file included in this repository
4. Make sure the ODBC driver for MySQL is installed on your system.
5. Open the project in Visual Studio 2022.
6. Run the project. If the database connection is successful, you'll see the message: "Koneksi Berhasil".
7. Use the interface to add, update, or delete data locally. Press Simpan to commit the changes to the database.

---
