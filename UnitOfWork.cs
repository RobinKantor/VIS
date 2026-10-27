using System;
using System.Collections.Generic;
using System.Text;
using vis3.DTO;
using vis3.Gateways;

namespace vis3
{
    public class UnitOfWork
    {
        private readonly CustomerGateway _customerGateway = new CustomerGateway();
        private readonly OrderGateway _orderGateway = new OrderGateway();

        private readonly List<CustomerDto> _newCustomers = new List<CustomerDto>();
        private readonly List<OrderDto> _newOrders = new List<OrderDto>();
        private readonly List<OrderDto> _modifiedOrders = new List<OrderDto>();

        public void RegisterNewCustomer(CustomerDto customer)
        {
            if (!_newCustomers.Contains(customer))
            {
                _newCustomers.Add(customer);
            }
        }

        public void RegisterNewOrder(OrderDto order)
        {
            if (!_newOrders.Contains(order))
            {
                _newOrders.Add(order);
            }
        }

        public void RegisterModifiedOrder(OrderDto order)
        {
            _modifiedOrders.Add(order);
        }

        public OrderDto FindNewOrderById(int id)
        {
            return _newOrders.FirstOrDefault(o => o.OrderId == id);
        }

        public void SaveChanges() 
        {
            Commit();
        }

        public void Commit()
        {
            try
            {
                foreach (var customer in _newCustomers) 
                {
                    _customerGateway.Insert(customer);
                }

                foreach (var order in _newOrders)
                {
                    _orderGateway.Insert(order);
                }

                foreach (var order in _modifiedOrders)
                {
                    _orderGateway.Update(order);
                }

                Clear();
            }
            catch (Exception ex) {
                Rollback();
                throw new Exception("Chyba pri commitu v UoW.", ex);
            }
        }

        public void Rollback()
        {
            Clear();
        }

        private void Clear()
        {
            _newCustomers.Clear();
            _newOrders.Clear();
            _modifiedOrders.Clear();
        }
    }
}
