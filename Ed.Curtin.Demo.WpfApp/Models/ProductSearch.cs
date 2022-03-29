namespace Ed.Curtin.Demo.WpfApp.Models
{
    /// <summary>
    /// ProductSearch - 
    /// </summary>
    public class ProductSearch
    {
        public ProductSearch(string sku = null, string title = null, string artist = null)
        {
            SKU = sku;
            Title = title;
            Artist = artist;
        }
        public string SKU { get; set; }
        public string Title { get; set; }

        public string Artist { get; set; }
    }
}
