using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ed.Curtin.Demo.WpfApp.DAL
{

    public static class StoredProcs
    {
        public const string SearchSKU = "[SP_Inventory_Product_Search_SKU]";

        public const string SearchTitle = "[SP_Inventory_Product_Search_Title]";

        public const string SearchArtist = "[SP_Inventory_Product_Search_Artist]";

        public const string UpdateProduct = "[SP_Inventory_Product_Update]";

        public const string SearchNoParams = "[SP_Inventory_Product_Search_No_Params]";
    }
}
