using System;
using System.Collections.Generic;
using FluorineFx;
using FluorineFx.AMF3;

namespace ServiceLibrary
{
    [RemotingService]
    public class MyService
    {
        public List<CustomerVO> GetCustomers()
        {
            List<CustomerVO> customers = new List<CustomerVO>();
            CustomerVO customer = new CustomerVO();
            customer.Firstname = "Ruben";
            customer.Lastname = "Alonso";
            PhoneVO phone = new PhoneVO();
            phone.AreaCode = "415";
            phone.Number = "555-0124";
            customer.PhoneNumbers.Add(phone);
            customers.Add(customer);
            return customers;
        }

        public void SetCustomers(List<CustomerVO> customers)
        {
        }

        public ArrayCollection GetCustomers2()
        {
            ArrayCollection customers = new ArrayCollection();
            CustomerVO customer = new CustomerVO();
            customer.Firstname = "Ruben";
            customer.Lastname = "Alonso";
            PhoneVO phone = new PhoneVO();
            phone.AreaCode = "415";
            phone.Number = "555-0124";
            customer.PhoneNumbers.Add(phone);
            customers.Add(customer);
            return customers;
        }

        public void SetCustomers2(ArrayCollection customers)
        {
        }
    }
}
