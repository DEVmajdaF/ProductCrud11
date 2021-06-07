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
    public class DeleteProductPage : ContentPage
    {
        private ListView _listView;
        private Button _button;

        Product product = new Product();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDb.db3");

        public DeleteProductPage()
        {
            this.Title = "Delete Product";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();


            _listView = new ListView();
            _listView.ItemsSource = db.Table<Product>().OrderBy(x => x.Name).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _button = new Button();
            _button.Text = "Delete";
            _button.Clicked += _button_Clicked;
            stackLayout.Children.Add(_button);

            Content = stackLayout;
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            product = (Product)e.SelectedItem;

        }

        private async void _button_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Table<Product>().Delete(x => x.Id == product.Id);

            await Navigation.PopAsync();

        }
    }
}