using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using vis3.DTO;

namespace vis3.Gateways
{
    public class CustomerGateway
    {
        public List<CustomerDto> FindAll()
        {
            var list = new List<CustomerDto>();
            string query = @"SELECT CustomerId, CustomerType, Email, Phone, BillingAddress, 
                                    FirstName, LastName, BirthDate, CompanyName, Ico, Dic, ContactPerson 
                             FROM CustomersSti";

            using (var connection = DbConnectionFactory.CreateConnection())
            using (var command = new SqliteCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string type = reader.GetString(1);

                        if (type == "P")
                        {
                            list.Add(new PersonCustomerDto
                            {
                                CustomerId = reader.GetInt32(0),
                                Email = reader.GetString(2),
                                Phone = reader.GetString(3),
                                BillingAddress = reader.GetString(4),
                                FirstName = reader.IsDBNull(5) ? null : reader.GetString(5),
                                LastName = reader.IsDBNull(6) ? null : reader.GetString(6),
                                BirthDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7))
                            });
                        }
                        else if (type == "C")
                        {
                            list.Add(new CompanyCustomerDto
                            {
                                CustomerId = reader.GetInt32(0),
                                Email = reader.GetString(2),
                                Phone = reader.GetString(3),
                                BillingAddress = reader.GetString(4),
                                CompanyName = reader.GetString(8),
                                Ico = reader.IsDBNull(9) ? null : reader.GetString(9),
                                Dic = reader.IsDBNull(10) ? null : reader.GetString(10),
                                ContactPerson = reader.IsDBNull(11) ? null : reader.GetString(11)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void Insert(CustomerDto dto)
        {
            string query = @"INSERT INTO CustomersSti (CustomerType, Email, Phone, BillingAddress, 
                                                       FirstName, LastName, BirthDate, CompanyName, Ico, Dic, ContactPerson) 
                             VALUES (@CustomerType, @Email, @Phone, @BillingAddress, 
                                     @FirstName, @LastName, @BirthDate, @CompanyName, @Ico, @Dic, @ContactPerson);
                             SELECT last_insert_rowid();";

            using (var connection = DbConnectionFactory.CreateConnection())
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CustomerType", dto.CustomerType);
                command.Parameters.AddWithValue("@Email", dto.Email);
                command.Parameters.AddWithValue("@Phone", dto.Phone);
                command.Parameters.AddWithValue("@BillingAddress", dto.BillingAddress);

                if (dto is PersonCustomerDto person)
                {
                    command.Parameters.AddWithValue("@FirstName", (object)person.FirstName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", (object)person.LastName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@BirthDate", person.BirthDate.HasValue ? person.BirthDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value);

                    command.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                    command.Parameters.AddWithValue("@Ico", DBNull.Value);
                    command.Parameters.AddWithValue("@Dic", DBNull.Value);
                    command.Parameters.AddWithValue("@ContactPerson", DBNull.Value);
                }
                else if (dto is CompanyCustomerDto company)
                {
                    command.Parameters.AddWithValue("@FirstName", DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", DBNull.Value);
                    command.Parameters.AddWithValue("@BirthDate", DBNull.Value);

                    command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    command.Parameters.AddWithValue("@Ico", (object)company.Ico ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Dic", (object)company.Dic ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ContactPerson", (object)company.ContactPerson ?? DBNull.Value);
                }

                connection.Open();

                long insertedId = (long)command.ExecuteScalar();
                dto.CustomerId = Convert.ToInt32(insertedId);
            }
        }

        public CustomerDto FindById(int id)
        {
            string query = @"SELECT CustomerId, CustomerType, Email, Phone, BillingAddress, 
                                    FirstName, LastName, BirthDate, CompanyName, Ico, Dic, ContactPerson 
                             FROM CustomersSti 
                             WHERE CustomerId = @Id";

            using (var connection = DbConnectionFactory.CreateConnection())
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string type = reader.GetString(1);

                        if (type == "P")
                        {
                            return new PersonCustomerDto
                            {
                                CustomerId = reader.GetInt32(0),
                                Email = reader.GetString(2),
                                Phone = reader.GetString(3),
                                BillingAddress = reader.GetString(4),
                                FirstName = reader.IsDBNull(5) ? null : reader.GetString(5),
                                LastName = reader.IsDBNull(6) ? null : reader.GetString(6),
                                BirthDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7))
                            };
                        }
                        else if (type == "C")
                        {
                            return new CompanyCustomerDto
                            {
                                CustomerId = reader.GetInt32(0),
                                Email = reader.GetString(2),
                                Phone = reader.GetString(3),
                                BillingAddress = reader.GetString(4),
                                CompanyName = reader.GetString(8),
                                Ico = reader.IsDBNull(9) ? null : reader.GetString(9),
                                Dic = reader.IsDBNull(10) ? null : reader.GetString(10),
                                ContactPerson = reader.IsDBNull(11) ? null : reader.GetString(11)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public int GetNextId()
        {
            var list = FindAll();
            return list.Count == 0 ? 1 : list.Max(c => c.CustomerId) + 1;
        }
    }
}
