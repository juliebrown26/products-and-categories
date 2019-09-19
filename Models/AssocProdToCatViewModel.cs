using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace productscategories.Models
{
    public class AssocProdToCatViewModel
    {
        public List<Product> Products { get; set; }

        public Category Category { get; set; }

        public Association Association { get; set; }
    }
}