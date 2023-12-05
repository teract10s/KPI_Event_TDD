using EventsLibrary.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary.model
{
    public class Event
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != null && value.Length > 1)
                {
                    name = value;
                }
                else
                {
                    throw new IncorrectInputData("Name can't be null or empty");
                }
            }
        }

        private string place;
        public string Place
        {
            get { return place; }
            set
            {
                if (value != null && value.Length > 1)
                {
                    place = value;
                }
                else
                {
                    throw new IncorrectInputData("Place can't be null or empty");
                }
            }
        }

        private bool isFavorite;
        public bool IsFavorite { get; set; }

        private EventCategory category;
        public EventCategory Category { get { return category;  } set { category = value; } }

        public Event () { }

        public Event (string name, string place, EventCategory category)
        {
            this.name = name;
            this.place = place;
            this.category = category;
            isFavorite = false;
        }

        private Event(string name, string place, EventCategory category, bool isFavorite)
        {
            this.name = name;
            this.place = place;
            this.category = category.Copy();
            this.isFavorite = isFavorite;
        }

        public Event Copy()
        {
            return new Event (name, place, category, isFavorite);
        }

        public string ToString()
        {
            return "\nEvent: "
                + "\n\t Name: " + name
                + "\n\t Place: " + place 
                + "\n\t Favorite: " + isFavorite
                + "\n\tCategory: "
                + "\n\t\t name: " + category.Name
                + "\n\t\t description: " + category.Description;
        }
    }
}
