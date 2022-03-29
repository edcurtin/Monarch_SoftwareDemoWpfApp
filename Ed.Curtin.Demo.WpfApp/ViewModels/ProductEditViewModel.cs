using Ed.Curtin.Demo.WpfApp.BaseClasses;
using Ed.Curtin.Demo.WpfApp.Models;
using Ed.Curtin.Demo.WpfApp.PubSub;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Ed.Curtin.Demo.WpfApp.ViewModels
{
    public class ProductEditViewModel : ViewModelBase, IDataErrorInfo
    {
        private Product _model;
        private IEventAggregator _eventAggregator;
        public ProductEditViewModel(Product model, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            IsProductEditEnabled = false;
            if (model != null)
            {
                IsProductEditEnabled = true;
                _model = model;
                ProductId = model.ProductId.ToString();

                Title = model.Title;

                SKU = model.SKU;

                Artist = model.Artist;
            }
        }

        private bool _isProductEditEnabled;
        public bool IsProductEditEnabled
        {
            get { return _isProductEditEnabled; }
            set
            {
                _isProductEditEnabled = value;
                OnPropertyChanged();
            }
        }

        public string ProductId { get; private set; }

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

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(param => PerformSave(), CanExecute);
            }
        }
        public bool CanExecute(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(this["SKU"]) || !string.IsNullOrWhiteSpace(this["Title"]) || !string.IsNullOrWhiteSpace(this["Artist"]))
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        private void PerformSave()
        {
            _model.Artist = Artist;
            _model.Title = Title;
            _model.SKU = SKU;
            _model.ModifiedDate = DateTime.Now;
            _eventAggregator.GetEvent<ProductEditEvent>().Publish(_model);
        }

        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(param => PerformCancel());
            }
        }


        public string Error
        {
            get
            {
                return null;
            }
        }


        public string this[string name]
        {
            get
            {
                string result = null;

                if (name == "SKU")
                {
                    if (SKU != null)
                    {
                        if (!SKU.All(char.IsDigit) || SKU.Length < 10)
                        {
                            // todo this would need to be added to resources
                            result = "SKU Incorrect Format";
                        }
                    }
                }

                if (name == "Title")
                {
                    if (Title != null)
                    {
                        if (string.IsNullOrEmpty(Title))
                        {
                            result = "Title Length is Invalid";
                        }
                    }
                }

                if (name == "Artist")
                {
                    if (Artist != null)
                    {
                        if (string.IsNullOrEmpty(Artist))
                        {
                            result = "Artist Length is Invalid";
                        }
                    }
                }
                return result;
            }
        }

        private void PerformCancel()
        {
            Title = _model.Title;
            SKU = _model.SKU;
            Artist = _model.Artist;
        }
    }
}
