using System.Runtime.Serialization;
using System;
using SATA.Global;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SATA.Model
{
    [DataContract]
    public class JCustomer
    {
        [DataMember]
        protected int customerID;
        [DataMember]
        protected string name;
        [DataMember]
        protected string email;
        [DataMember]
        protected string mobile;
        [DataMember]
        protected JTransaction[] transactions;

        public JCustomer SetCustomer(Customer customer)
        {
            this.customerID = customer.ID;
            this.name = customer.Name;
            this.email = customer.Email;
            this.mobile = customer.MobileNo;
            this.transactions = customer.Transactions?.Select(x => new JTransaction().SetTransaction(x)).ToArray();

            return this;
        }


        public Customer GetCustomer()
        {
            Customer customer = new Customer();
            customer.ID = customerID;
            customer.Name = name;
            customer.MobileNo = mobile;
            if (Regex.IsMatch(email, Static.RegexEmail))
            {
                customer.Email = email;
            }
            else
            {
                throw new Exception("Incorect email");
            }
            customer.Transactions = transactions?.Select(x => x.GetTransaction(customer)).ToArray();

            return customer;
        }
    }

    [DataContract]
    public class JTransaction
    {
        [DataMember]
        protected int id;
        [DataMember]
        protected string date;
        [DataMember]
        protected decimal amount;
        [DataMember]
        protected string currency;
        [DataMember]
        protected string status;

        public JTransaction SetTransaction(Transaction transaction)
        {
            this.id = transaction.ID;
            this.date = transaction.TransactionDateTime.ToString(Static.DataFormat);
            this.amount = transaction.Amount;
            this.currency = transaction.CurrencyCode;
            this.status = transaction.Status.ToString();

            return this;
        }

        public Transaction GetTransaction(Customer customer = null)
        {
            Transaction transaction = new Transaction();
            transaction.ID = id;
            transaction.Customer = customer;
            transaction.Amount = amount;
            if (DateTime.TryParseExact(date, Static.DataFormat, null, DateTimeStyles.None, out DateTime pDate))
            {
                transaction.TransactionDateTime = pDate;
            }
            else
            {
                throw new Exception("Incorect Date");
            }
            transaction.CurrencyCode = currency?.ToUpper();
            if (Enum.TryParse(status, out TransactionStatus pStatus))
            {
                transaction.Status = pStatus;
            }
            else
            {
                throw new Exception("Incorect Transaction Status");
            }
            return transaction;
        }

    }
}
