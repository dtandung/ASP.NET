using _19T1021044.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021044.Web.Models
{
    public class CustomerSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// danh sách khách hàng
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}