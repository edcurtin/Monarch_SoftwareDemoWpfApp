
using System;

namespace Ed.Curtin.Demo.WpfApp.Models
{
    public class DataTableRow
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int FreeStock { get; set; }

    }
}
