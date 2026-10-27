using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using vis3.DTO;
using vis3.Gateways;

namespace vis3
{
    public class OrderService
    {
        private readonly CustomerGateway _customerGateway = new CustomerGateway();
        private readonly OrderGateway _orderGateway = new OrderGateway();

        List<Customer> Customers { get; set; } = new List<Customer>();
        List<Order> Orders { get; set; } = new List<Order>();

        public PersonCustomerDto CreatePersonCustomer(string firstName, string lastName, DateTime? birthDate, string email, string phone, string billingAddress, UnitOfWork uow)
        {
            PersonCustomerDto c = new PersonCustomerDto
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                Email = email,
                Phone = phone,
                BillingAddress = billingAddress
            };

            uow.RegisterNewCustomer(c);
            return c;
        }

        public CompanyCustomerDto CreateCompanyCustomer(string companyName, string ico, string dic, string contactPerson, string email, string phone, string billingAddress, UnitOfWork uow)
        {
            CompanyCustomerDto c = new CompanyCustomerDto
            {
                CompanyName = companyName,
                Ico = ico,
                Dic = dic,
                ContactPerson = contactPerson,
                Email = email,
                Phone = phone,
                BillingAddress = billingAddress
            };

            uow.RegisterNewCustomer(c);
            return c;
        }

        public OrderDto CreateOrder(
            int customerId,
            int width,
            int height,
            int depth,
            string material,
            string notes,
            UnitOfWork uow
            )
        {
            if (width <= 0 || height <= 0 || depth <= 0)
            {
                throw new ArgumentException("Rozměry musí být větší než 0!");
            }

            OrderDto o = new OrderDto
            {
                CustomerId = customerId,
                Width = width,
                Height = height,
                Depth = depth,
                Material = material,
                Notes = notes,
                Status = "CEKA_NA_POTVRZENI"
            };

            //_orderGateway.Insert(o);
            uow.RegisterNewOrder(o);

            return o;
        }

        public void ConfirmOrder(int orderId, UnitOfWork uow)
        {
            OrderDto foundOrder = uow.FindNewOrderById(orderId);

            if (foundOrder == null)
            {
                foundOrder = _orderGateway.FindById(orderId);
            }

            if (foundOrder != null)
            {
                foundOrder.Status = "POTVRZENO";

                //_orderGateway.Update(foundOrder);
                uow.RegisterModifiedOrder(foundOrder);
            }
        }

        public void SaveChanges(UnitOfWork uow)
        {
            uow.Commit();
        }

        public void PrintOrder(int orderId)
        {
            OrderDto foundOrder = _orderGateway.FindById(orderId);

            if (foundOrder != null)
            {
                Console.WriteLine($"ID: {foundOrder.OrderId} | Stav: {foundOrder.Status} | Rozměry: {foundOrder.Width}x{foundOrder.Height}x{foundOrder.Depth}");
            }
            else
            {
                Console.WriteLine($"Objednávka s ID {orderId} nebyla nalezena.");
            }
        }

        public void ListOrders()
        {
            var allOrders = _orderGateway.FindAll();

            foreach (OrderDto o in allOrders)
            {
                Console.WriteLine($"ID: {o.OrderId} | Zákazník ID: {o.CustomerId} | Stav: {o.Status} | Materiál: {o.Material}");
            }
        }
    }
}
