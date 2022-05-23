using SpaceStation.Models.Bags.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Bags
{
    public class Bag : IBag
    {
        private List<string> items;

        public Bag()
        {
            this.items = new List<string>();
        }
        public ICollection<string> Items
        {
            get { return this.items.ToList(); }
        }
    }
}
