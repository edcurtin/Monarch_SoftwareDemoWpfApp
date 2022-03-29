using Ed.Curtin.Demo.WpfApp.BaseClasses;
using Ed.Curtin.Demo.WpfApp.DAL;
using LightInject;
using Prism.Events;
using System;

namespace Ed.Curtin.Demo.WpfApp.ViewModels
{
    public class WrapperMainWindowViewModel : ViewModelBase
    {
        private ServiceContainer _container;

        public WrapperMainWindowViewModel()
        {
            _container = new LightInject.ServiceContainer();
            RegisterTypesWithIoc();
            var eventAggregator = _container.GetInstance<IEventAggregator>();
            ProductSearchVm = new ProductSearchViewModel(eventAggregator);
            ProductGridAndEditVm = new ProductGridAndEditViewModel(_container.GetInstance<IProductInventory>(), eventAggregator);
        }

        private void RegisterTypesWithIoc()
        {

            _container.Register<IProductInventory, ProductInventory>();
            _container.Register<IEventAggregator, EventAggregator>();
        }

        private ProductGridAndEditViewModel _productGridAndEditVm;
        public ProductGridAndEditViewModel ProductGridAndEditVm
        {
            get
            {
                return _productGridAndEditVm;
            }
            set
            {
                _productGridAndEditVm = value;
                OnPropertyChanged();
            }
        }

        private ProductSearchViewModel _productSearchVm;
        public ProductSearchViewModel ProductSearchVm
        {
            get { return _productSearchVm; }
            set
            {
                _productSearchVm = value;
                OnPropertyChanged();
            }
        }
    }
}
