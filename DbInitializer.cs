using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public static class DbConnectionFactory
    {
        private const string ConnectionString = "Data Source=database.db;Foreign Keys=True;";

        public static SqliteConnection CreateConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }

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

                //string dropTablesQuery = "DROP TABLE IF EXISTS Orders; DROP TABLE IF EXISTS CustomersSti;";
                //using (var command = new SqliteCommand(dropTablesQuery, connection))
                //{
                //    command.ExecuteNonQuery();
                //}

                using (var command = new SqliteCommand("PRAGMA foreign_keys = ON;", connection))
                {
                    command.ExecuteNonQuery();
                }

                string createCustomersStiTable = @"
                    CREATE TABLE IF NOT EXISTS CustomersSti (
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
                    CREATE TABLE IF NOT EXISTS Orders (
                        OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerId INTEGER NOT NULL,
                        Width INTEGER NOT NULL,
                        Height INTEGER NOT NULL,
                        Depth INTEGER NOT NULL,
                        Material TEXT NULL,
                        Notes TEXT NULL,
                        Status TEXT NOT NULL,
                        Number INTEGER NOT NULL,
                        CalculatedPrice DECIMAL(18, 2) NULL,
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