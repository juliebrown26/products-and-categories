using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace productscategories.Models
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; }
        public Category NewCategory { get; set; }
    }
}