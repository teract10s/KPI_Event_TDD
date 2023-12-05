using EventsLibrary.exception;

namespace EventsLibrary.model
{
    public class EventCategory
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

        private string description; 
        public string Description 
        {
            get { return description; }
            set
            {
                if (value != null && value.Length > 1)
                {
                    description = value;
                }
                else
                {
                    throw new IncorrectInputData("Description can't be null or empty");
                }
            }
        }

        public EventCategory() { }

        public EventCategory(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string ToString()
        {
            return "\nCategory: " 
                + "\n\t name: " + name
                + "\n\t description: " + description;
        }

        public EventCategory Copy()
        {
            return new EventCategory(name, description);
        }
    }
}
