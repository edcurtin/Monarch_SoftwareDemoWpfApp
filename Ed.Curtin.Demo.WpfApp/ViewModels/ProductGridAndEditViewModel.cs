using Ed.Curtin.Demo.WpfApp.BaseClasses;
using Ed.Curtin.Demo.WpfApp.DAL;
using Ed.Curtin.Demo.WpfApp.Models;
using Ed.Curtin.Demo.WpfApp.PubSub;
using Prism.Events;
using System;

namespace Ed.Curtin.Demo.WpfApp.ViewModels
{
    public class ProductGridAndEditViewModel : ViewModelBase, IDisposable
    {
        private Product _selectedProductModel;
        private IProductInventory _productInventory;
        private IEventAggregator _eventAggregator;
        private SubscriptionToken ProductDataTableSelectedItemEventSubscriptionToken;
        private SubscriptionToken ProductEditEventSubscriptionToken;

        public ProductGridAndEditViewModel(IProductInventory productInventory, IEventAggregator eventAggregator)
        {
            _productInventory = productInventory;
            _eventAggregator = eventAggregator;
            ProductDataGridVm = new ProductDataGridViewModel(_productInventory, _eventAggregator);


            ProductDataTableSelectedItemEvent dataGridSelectedItemChangedEvent = eventAggregator.GetEvent<ProductDataTableSelectedItemEvent>();
            if (ProductDataTableSelectedItemEventSubscriptionToken != null)
            {
                dataGridSelectedItemChangedEvent.Unsubscribe(HandleDataGridSelectedItemChangedEvent);
            }

            ProductDataTableSelectedItemEventSubscriptionToken = dataGridSelectedItemChangedEvent.Subscribe((selectedDataTableRow) => HandleDataGridSelectedItemChangedEvent(selectedDataTableRow), ThreadOption.UIThread, true);

            ProductEditEvent productEditEvent = eventAggregator.GetEvent<ProductEditEvent>();
            if(ProductEditEventSubscriptionToken != null)
            {
                productEditEvent.Unsubscribe(HandleProductEditEvent);
            }
            ProductEditEventSubscriptionToken = productEditEvent.Subscribe((productBeingEdited) => HandleProductEditEvent(productBeingEdited), ThreadOption.UIThread, true);

            ProductEditVm = new ProductEditViewModel(null, _eventAggregator);
        }

        private ProductDataGridViewModel _productDataGridVm;
        public ProductDataGridViewModel ProductDataGridVm
        {
            get { return _productDataGridVm; }
            set
            {
                _productDataGridVm = value;
                OnPropertyChanged();
            }
        }

        private ProductEditViewModel _productEditVm;
        public ProductEditViewModel ProductEditVm
        {
            get { return _productEditVm; }
            set
            {
                _productEditVm = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// HandleProductEditEvent - PubSub event triggered in ProductEditViewModel - we call the dal update a product and recall search with empty model
        /// </summary>
        /// <param name="productBeingEdited"></param>
        private void HandleProductEditEvent(Product productBeingEdited)
        {
            if (productBeingEdited != null)
            {
                _productInventory.UpdateProduct(productBeingEdited);
                _eventAggregator.GetEvent<ProductSearchEvent>().Publish(new ProductSearch());
            }

        }

        /// <summary>
        /// HandleDataGridSelectedItemChangedEvent - PubSub Event triggered from ProductDataGridViewModel
        /// </summary>
        /// <param name="dataTableRow">DataGrids SelectedItem</param>
        private void HandleDataGridSelectedItemChangedEvent(DataTableRow dataTableRow)
        {

            if (dataTableRow != null)
            {
                _selectedProductModel = new Product();
                _selectedProductModel.ProductId = dataTableRow.ProductId;
                _selectedProductModel.Artist = dataTableRow.Artist;
                _selectedProductModel.SKU = dataTableRow.SKU;
                _selectedProductModel.Title = dataTableRow.Title;
                _selectedProductModel.ModifiedDate = dataTableRow.ModifiedDate;
                _selectedProductModel.CreatedDate = dataTableRow.CreatedDate;
                ProductEditVm = new ProductEditViewModel(_selectedProductModel, _eventAggregator);
            }
            else
            {
                ProductEditVm = new ProductEditViewModel(null, _eventAggregator);
            }

        }

        public void Dispose()
        {
            _eventAggregator.GetEvent<ProductDataTableSelectedItemEvent>().Unsubscribe(HandleDataGridSelectedItemChangedEvent);
            _eventAggregator.GetEvent<ProductEditEvent>().Unsubscribe(HandleProductEditEvent);
        }
    }
}
