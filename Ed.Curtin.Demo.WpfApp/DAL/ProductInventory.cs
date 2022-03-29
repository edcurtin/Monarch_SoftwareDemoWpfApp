
using Dapper;
using Ed.Curtin.Demo.WpfApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Ed.Curtin.Demo.WpfApp.DAL
{
    public class ProductInventory : IProductInventory
    {
        // ideally this would be a appconfig setting
        private string _connectionString = @"Server=(LocalDb)\MSSQLLocalDB;Database=Inventory;Trusted_Connection=True";



        public List<DataTableRow> SearchProducts(ProductSearch productSearch)
        {
            List<DataTableRow> results = new List<DataTableRow>();
            SqlConnection _conn = new SqlConnection(_connectionString);
            using (_conn)
            {
                _conn.Open();

                if (string.IsNullOrWhiteSpace(productSearch.Artist) && string.IsNullOrWhiteSpace(productSearch.SKU) && string.IsNullOrWhiteSpace(productSearch.Title))
                {
                    results = _conn.Query<DataTableRow>(StoredProcs.SearchNoParams, commandType: CommandType.StoredProcedure).AsList();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(productSearch.SKU))
                    {
                        var values = new { SKU = productSearch.SKU };
                        results = _conn.Query<DataTableRow>(StoredProcs.SearchSKU, values, commandType: CommandType.StoredProcedure).AsList();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(productSearch.Title))
                        {
                            var values = new { Title = productSearch.Title };
                            results = _conn.Query<DataTableRow>(StoredProcs.SearchTitle, values, commandType: CommandType.StoredProcedure).AsList();
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(productSearch.Artist))
                            {
                                var values = new { Artist = productSearch.Artist };
                                results = _conn.Query<DataTableRow>(StoredProcs.SearchArtist, values, commandType: CommandType.StoredProcedure).AsList();
                            }
                        }

                    }
                }
                _conn.Close();
                return results;
            }
        }

        public void UpdateProduct(Product product)
        {
            SqlConnection _conn = new SqlConnection(_connectionString);
            using (_conn)
            {
                var valuesToUpdate = new { ProductId = product.ProductId, SKU = product.SKU, Artist = product.Artist, Title = product.Title };
                _conn.Query(StoredProcs.UpdateProduct, valuesToUpdate, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
