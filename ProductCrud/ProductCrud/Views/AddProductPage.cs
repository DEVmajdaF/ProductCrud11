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
    public class AddProductPage : ContentPage
    {
        private Entry _nameEntry;
        private Entry _descriptionEntry;
        private Entry _priceEntry;
        private Button _saveButton;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDb.db3");


        public AddProductPage()
        {
            this.Title = "Add Product";
            StackLayout stackLayout = new StackLayout();

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

            _saveButton = new Button();
            _saveButton.Text = "Add";
            _saveButton.Clicked += _saveButton_Clicked;
            stackLayout.Children.Add(_saveButton);

            Content = stackLayout;

        }

        private async void _saveButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Product>();

            var maxPk = db.Table<Product>().OrderByDescending(c => c.Id).FirstOrDefault();

            Product product = new Product()
            {

                Id = (maxPk == null ? 1 : maxPk.Id + 1),
                Name = _nameEntry.Text,
                Description = _descriptionEntry.Text,
                Price = _priceEntry.Text,

            };
            db.Insert(product);
            await DisplayAlert(null, product.Name + "Saved", "Ok");
            await Navigation.PopAsync();

        }
    }
}