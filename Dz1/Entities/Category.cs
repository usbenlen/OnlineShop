using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dz1.Entities;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
