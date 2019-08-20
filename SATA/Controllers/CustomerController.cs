using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SATA.Model;
using System.Text.RegularExpressions;
using SATA.Global;
using Newtonsoft.Json.Linq;

namespace SATA.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Context _context;
        public CustomerController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Post(dynamic value)
        {
            try
            {               
                dynamic customerID = value.customerID;
                dynamic email = value.email;            

                if (customerID == null && email == null)
                {
                    return StatusCode(400, "No inquiry criteria");
                }

                IQueryable<Customer> customers = _context.Customer;

                if (customerID != null)
                {
                    var id = customerID.Value as long?;
                    if (id != null)
                        customers = from c in customers
                                    where c.ID == id
                                    select c;
                    else
                        return StatusCode(400, "Invalid Customer ID");
                }

                if (email != null)
                {
                    var e = email.Value as string;
                    if (Regex.IsMatch(e , Static.RegexEmail))
                        customers = from c in _context.Customer
                                    where c.Email == e
                                    select c;
                    else 
                        return StatusCode(400, "Invalid Email");
                }

                Customer result = customers.FirstOrDefault();

                if (result != null)
                {
                    result.Transactions = _context.Transaction.Where(x => x.Customer.ID == result.ID).Take(5).ToArray();
                }
                else
                {
                    return StatusCode(404);
                }

                return Ok(new JCustomer().SetCustomer(result));
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(JCustomer value)
        {
            try
            {
                var test = value.GetCustomer();
                _context.Customer.Add(test);
                var count = _context.SaveChanges();

                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}