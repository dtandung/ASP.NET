using _19T1021044.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021044.Web.Models
{
    public class OrderSearchOutput : PaginationOrderSearchOutput
    {
        public List<Order> Data { get; set; }
    }
}