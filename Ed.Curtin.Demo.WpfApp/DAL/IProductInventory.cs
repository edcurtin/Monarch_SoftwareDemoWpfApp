using Ed.Curtin.Demo.WpfApp.Models;
using System.Collections.Generic;

namespace Ed.Curtin.Demo.WpfApp.DAL
{
    public interface IProductInventory
    {
        /// <summary>
        /// SearchProducts - Populate Product Search Object and Call Search Stored Proc
        /// </summary>
        /// <param name="productSearch"></param>
        /// <returns></returns>
        List<DataTableRow> SearchProducts(ProductSearch productSearch);

        /// <summary>
        /// UpdateProduct - Will Call the Edit Stored Proc
        /// </summary>
        /// <param name="product"></param>
        void UpdateProduct(Product product);
    }
}
