using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using vis3.DTO;

namespace vis3.Gateways
{
    public class OrderGateway
    {
        public List<OrderDto> FindAll()
        {
            var list = new List<OrderDto>();
            string query = "SELECT OrderId, CustomerId, Width, Height, Depth, Material, Notes, Status FROM Orders";

            using (var connection = DbConnectionFactory.CreateConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new OrderDto
                        {
                            OrderId = reader.GetInt32(0),
                            CustomerId = reader.GetInt32(1),
                            Width = reader.GetInt32(2),
                            Height = reader.GetInt32(3),
                            Depth = reader.GetInt32(4),
                            Material = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Notes = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Status = reader.GetString(7)
                        });
                    }
                }
            }
            return list;
        }

        public void Insert(OrderDto dto)
        {
            string query = @"
                INSERT INTO Orders (CustomerId, Width, Height, Depth, Material, Notes, Status)
                VALUES (@CustomerId, @Width, @Height, @Depth, @Material, @Notes, @Status)";

            using (var connection = DbConnectionFactory.CreateConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CustomerId", dto.CustomerId);
                command.Parameters.AddWithValue("@Width", dto.Width);
                command.Parameters.AddWithValue("@Height", dto.Height);
                command.Parameters.AddWithValue("@Depth", dto.Depth);
                command.Parameters.AddWithValue("@Material", (object)dto.Material ?? DBNull.Value);
                command.Parameters.AddWithValue("@Notes", (object)dto.Notes ?? DBNull.Value);
                command.Parameters.AddWithValue("@Status", dto.Status);

                connection.Open();
                command.ExecuteNonQuery();

                dto.OrderId = Convert.ToInt32(command.LastInsertedId);
            }
        }

        public OrderDto FindById(int id)
        {
            string query = "SELECT OrderId, CustomerId, Width, Height, Depth, Material, Notes, Status FROM Orders WHERE OrderId = @Id";

            using (var connection = DbConnectionFactory.CreateConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new OrderDto
                        {
                            OrderId = reader.GetInt32(0),
                            CustomerId = reader.GetInt32(1),
                            Width = reader.GetInt32(2),
                            Height = reader.GetInt32(3),
                            Depth = reader.GetInt32(4),
                            Material = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Notes = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Status = reader.GetString(7)
                        };
                    }
                }
            }
            return null;
        }

        public void Update(OrderDto dto)
        {
            string query = @"
                UPDATE Orders 
                SET CustomerId = @CustomerId, Width = @Width, Height = @Height, Depth = @Depth, 
                    Material = @Material, Notes = @Notes, Status = @Status 
                WHERE OrderId = @OrderId";

            using (var connection = DbConnectionFactory.CreateConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderId", dto.OrderId);
                command.Parameters.AddWithValue("@CustomerId", dto.CustomerId);
                command.Parameters.AddWithValue("@Width", dto.Width);
                command.Parameters.AddWithValue("@Height", dto.Height);
                command.Parameters.AddWithValue("@Depth", dto.Depth);
                command.Parameters.AddWithValue("@Material", (object)dto.Material ?? DBNull.Value);
                command.Parameters.AddWithValue("@Notes", (object)dto.Notes ?? DBNull.Value);
                command.Parameters.AddWithValue("@Status", dto.Status);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
