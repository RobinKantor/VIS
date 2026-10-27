using System;
using System.Collections.Generic;
using System.Text;

namespace vis3.DTO
{
    public abstract class CustomerDto
    {
        public int CustomerId { get; set; }
        public abstract string CustomerType { get; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
    }

    public class PersonCustomerDto : CustomerDto
    {
        public override string CustomerType => "P";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class CompanyCustomerDto : CustomerDto
    {
        public override string CustomerType => "C";
        public string CompanyName { get; set; }
        public string Ico { get; set; }
        public string Dic { get; set; }
        public string ContactPerson { get; set; }
    }
}
