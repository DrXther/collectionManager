using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolekcje.Models
{
    internal class Collection : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    onPropertyChanged(nameof(Id));
                }
            }
        }

        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set
            {
                if (itemName != value)
                {
                    itemName = value;
                    onPropertyChanged(nameof(ItemName));
                }
            }
        }


        private string itemState;
        public string ItemState
        {
            get { return itemState; }
            set
            {
                if (itemState != value)
                {
                    itemState = value;
                    onPropertyChanged(nameof(ItemState));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
