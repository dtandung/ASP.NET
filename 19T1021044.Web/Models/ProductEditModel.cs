using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021044.DomainModels;

namespace _19T1021044.Web.Models
{
    public class ProductEditModel
    {
        public Product Product { get; set; }
        public List<ProductAttribute> Attributes { get; set; }
        public List<ProductPhoto> Photos { get; set; }
        
    }
}