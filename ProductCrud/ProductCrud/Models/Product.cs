
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace CrudXamarin.Models
{
    class Product
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }

        public override string ToString()
        {
            return this.Name +"("+ this.Description+ ",  the price is :  " + this.Price+")";
        }

    }
}
