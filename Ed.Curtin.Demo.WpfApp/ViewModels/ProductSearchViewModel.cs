using Ed.Curtin.Demo.WpfApp.BaseClasses;
using Ed.Curtin.Demo.WpfApp.Models;
using Ed.Curtin.Demo.WpfApp.PubSub;
using Prism.Events;
using System.Windows.Input;

namespace Ed.Curtin.Demo.WpfApp.ViewModels
{
    /// <summary>
    /// ProductSearchViewModel
    /// </summary>
    public class ProductSearchViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        public ProductSearchViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        private string _sku;
        public string SKU
        {
            get { return _sku; }
            set
            {
                _sku = value.Trim().ToUpper();
                OnPropertyChanged();
            }
        }

        private string _artist;
        public string Artist
        {
            get
            {
                return _artist;
            }
            set
            {
                _artist = value.Trim().ToUpper();
                OnPropertyChanged();
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value.Trim().ToUpper();
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(param => PerformSearch());
            }
        }

        /// <summary>
        /// PerformSearch - called by ICommand and will use Prism to Publish the Product Search Model to listener
        /// </summary>
        private void PerformSearch()
        {
            ProductSearch searchModel = new ProductSearch(SKU, Title, Artist);
            _eventAggregator.GetEvent<ProductSearchEvent>().Publish(searchModel);
        }
    }
}
