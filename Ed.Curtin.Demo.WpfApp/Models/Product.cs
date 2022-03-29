using System;

namespace Ed.Curtin.Demo.WpfApp.Models
{
    /// <summary>
    /// Product - Product Model 
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
