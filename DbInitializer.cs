using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    //public static class DbConnectionFactory
    //{
    //    private const string Server = "dbsys.cs.vsb.cz";
    //    private const string Port = "3306";
    //    public const string Username = "KAN0310";
    //    private const string Password = "hvewQMq8q28Os9pg";
    //    public const string DatabaseName = "KAN0310";

    //    public static MySqlConnection CreateConnection()
    //    {
    //        string connString = $"Server={Server};Port={Port};Database={DatabaseName};Uid={Username};Pwd={Password};AllowUserVariables=True;";
    //        return new MySqlConnection(connString);
    //    }
    //}

    public static class DbConnectionFactory
    {
        private const string ConnectionString = "Data Source=database.db;";

        public static SqliteConnection CreateConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }

    //public static class DbInitializer
    //{
    //    public static void Initialize()
    //    {
    //        using (var connection = DbConnectionFactory.CreateConnection())
    //        {
    //            connection.Open();

    //            string dropTablesQuery = @"
    //                SET FOREIGN_KEY_CHECKS = 0;
    //                DROP TABLE IF EXISTS Orders;
    //                DROP TABLE IF EXISTS CustomersSti;
    //                SET FOREIGN_KEY_CHECKS = 1;";

    //            using (var command = new MySqlCommand(dropTablesQuery, connection))
    //            {
    //                command.ExecuteNonQuery();
    //            }

    //            //string createCustomersTable = @"
    //            //    CREATE TABLE Customers (
    //            //        CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    //            //        CustomerType VARCHAR(1) NOT NULL,
    //            //        Name VARCHAR(150) NOT NULL,
    //            //        Email VARCHAR(150) NOT NULL,
    //            //        LastName VARCHAR(150) NULL,
    //            //        BirthDate DATETIME NULL,
    //            //        CompanyName VARCHAR(150) NULL,
    //            //        Ico VARCHAR(50) NULL
    //            //    );";

    //            string createCustomersStiTable = @"
    //                CREATE TABLE CustomersSti (
    //                    CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    //                    CustomerType VARCHAR(1) NOT NULL,
    //                    Email VARCHAR(150) NOT NULL,
    //                    Phone VARCHAR(150) NOT NULL,
    //                    BillingAddress VARCHAR(150) NOT NULL,
    //                    FirstName VARCHAR(150) NOT NULL,
    //                    LastName VARCHAR(150) NULL,
    //                    BirthDate DATETIME NULL,
    //                    CompanyName VARCHAR(150) NULL,
    //                    Ico VARCHAR(50) NULL,
    //                    Dic VARCHAR(50) NULL,
    //                    ContactPerson VARCHAR(150) NULL
    //                );";

    //            using (var command = new MySqlCommand(createCustomersStiTable, connection))
    //            {
    //                command.ExecuteNonQuery();
    //            }

    //            string createOrdersTable = @"
    //                CREATE TABLE IF NOT EXISTS Orders (
    //                    OrderId INT AUTO_INCREMENT PRIMARY KEY,
    //                    CustomerId INT NOT NULL,
    //                    Width INT NOT NULL,
    //                    Height INT NOT NULL,
    //                    Depth INT NOT NULL,
    //                    Material VARCHAR(100) NULL,
    //                    Notes TEXT NULL,
    //                    Status VARCHAR(50) NOT NULL,
    //                    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) 
    //                        REFERENCES CustomersSti(CustomerId) ON DELETE CASCADE
    //                );";

    //            using (var command = new MySqlCommand(createOrdersTable, connection))
    //            {
    //                command.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //}

    public static class DbInitializer
    {
        public static void Initialize()
        {
            using (var connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = new SqliteCommand("PRAGMA foreign_keys = OFF;", connection))
                {
                    command.ExecuteNonQuery();
                }

                string dropTablesQuery = "DROP TABLE IF EXISTS Orders; DROP TABLE IF EXISTS CustomersSti;";
                using (var command = new SqliteCommand(dropTablesQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqliteCommand("PRAGMA foreign_keys = ON;", connection))
                {
                    command.ExecuteNonQuery();
                }

                string createCustomersStiTable = @"
                    CREATE TABLE CustomersSti (
                        CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerType TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Phone TEXT NOT NULL,
                        BillingAddress TEXT NOT NULL,
                        FirstName TEXT NULL,
                        LastName TEXT NULL,
                        BirthDate TEXT NULL,
                        CompanyName TEXT NULL,
                        Ico TEXT NULL,
                        Dic TEXT NULL,
                        ContactPerson TEXT NULL
                    );";

                using (var command = new SqliteCommand(createCustomersStiTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                string createOrdersTable = @"
                    CREATE TABLE Orders (
                        OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerId INTEGER NOT NULL,
                        Width INTEGER NOT NULL,
                        Height INTEGER NOT NULL,
                        Depth INTEGER NOT NULL,
                        Material TEXT NULL,
                        Notes TEXT NULL,
                        Status TEXT NOT NULL,
                        CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) 
                            REFERENCES CustomersSti(CustomerId) ON DELETE CASCADE
                    );";

                using (var command = new SqliteCommand(createOrdersTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}