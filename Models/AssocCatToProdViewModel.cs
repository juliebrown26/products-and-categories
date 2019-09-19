using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace productscategories.Models
{
    public class AssocCattoProdViewModel
    {
        public List<Category> Categories {get;set;}
        public Product Product {get;set;}
        public Association Association {get; set;}
    }
}