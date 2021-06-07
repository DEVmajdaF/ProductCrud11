using CrudXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ProductCrud.Views
{
    public class EditProductPage : ContentPage
    {
        private ListView _listView;
        private Entry _idEntry;
        private Entry _nameEntry;
        private Entry _descriptionEntry;
        private Entry _priceEntry;
        private Button _button;

        Product product = new Product();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDb.db3");

        public EditProductPage()
        {
            this.Title = "Edit Product";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();


            _listView = new ListView();
            _listView.ItemsSource = db.Table<Product>().OrderBy(x => x.Name).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _idEntry = new Entry();
            _idEntry.Placeholder = "ID";
            _idEntry.IsVisible = false;
            stackLayout.Children.Add(_idEntry);

            _nameEntry = new Entry();
            _nameEntry.Keyboard = Keyboard.Text;
            _nameEntry.Placeholder = "Product Name";
            stackLayout.Children.Add(_nameEntry);

            _descriptionEntry = new Entry();
            _descriptionEntry.Keyboard = Keyboard.Text;
            _descriptionEntry.Placeholder = "Description";
            stackLayout.Children.Add(_descriptionEntry);

            _priceEntry = new Entry();
            _priceEntry.Keyboard = Keyboard.Text;
            _priceEntry.Placeholder = "Price";
            stackLayout.Children.Add(_priceEntry);

            _button = new Button();
            _button.Text = "Update";
            _button.Clicked += _button_Clicked;
            stackLayout.Children.Add(_button);




            Content = stackLayout;

        }

        private async void _button_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            Product product = new Product()
            {
                Id = Convert.ToInt32(_idEntry.Text),
                Name = _nameEntry.Text,
                Description = _descriptionEntry.Text,
                Price = _priceEntry.Text,
            };

            db.Update(product);
            await Navigation.PopAsync();

        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            product = (Product)e.SelectedItem;
            _idEntry.Text = product.Id.ToString();
            _nameEntry.Text = product.Name;
            _descriptionEntry.Text = product.Description;
            _priceEntry.Text = product.Price;
        }
    }
}