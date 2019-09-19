using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace productscategories.Models
{
    public class IndexViewModel
    {
        public List<Product> Products { get; set; }
        public Product NewProduct { get; set; }
    }
}