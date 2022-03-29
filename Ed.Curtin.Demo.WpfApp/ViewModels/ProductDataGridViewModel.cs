using Ed.Curtin.Demo.WpfApp.BaseClasses;
using Ed.Curtin.Demo.WpfApp.DAL;
using Ed.Curtin.Demo.WpfApp.Models;
using Ed.Curtin.Demo.WpfApp.PubSub;
using Prism.Events;
using System;
using System.Collections.ObjectModel;

namespace Ed.Curtin.Demo.WpfApp.ViewModels
{
    public class ProductDataGridViewModel : ViewModelBase, IDisposable
    {
        private IProductInventory _productInventory;
        private IEventAggregator _eventAggregator;
        private SubscriptionToken ProductSearchEventSubscriptionToken;

        public ProductDataGridViewModel(IProductInventory productInventory, IEventAggregator eventAggregator)
        {
            _productInventory = productInventory;
            _eventAggregator = eventAggregator;
            var productsList = _productInventory.SearchProducts(new ProductSearch());
            Products = new ObservableCollection<DataTableRow>(productsList);
            ProductSearchEvent productSearchEvent = eventAggregator.GetEvent<ProductSearchEvent>();
            if (ProductSearchEventSubscriptionToken != null)
            {
                productSearchEvent.Unsubscribe(HandleProductSearchEventReceived);
            }

            ProductSearchEventSubscriptionToken = productSearchEvent.Subscribe((productSearch) => HandleProductSearchEventReceived(productSearch), ThreadOption.UIThread, true);
        }


        private DataTableRow _selectedProduct;
        public DataTableRow SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                _eventAggregator.GetEvent<ProductDataTableSelectedItemEvent>().Publish(value);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DataTableRow> _products;
        public ObservableCollection<DataTableRow> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// HandleProductSearchEventReceived - ProductSearchViewModel populates a search model and Transmits it via pub sub 
        /// </summary>
        /// <param name="productSearch">model populated in ProductSearchViewModel</param>
        private void HandleProductSearchEventReceived(ProductSearch productSearch)
        {
            var productsList = _productInventory.SearchProducts(productSearch);
            Products = new ObservableCollection<DataTableRow>(productsList);
        }

        public void Dispose()
        {
            _eventAggregator.GetEvent<ProductSearchEvent>().Unsubscribe(HandleProductSearchEventReceived);
        }
    }
}
