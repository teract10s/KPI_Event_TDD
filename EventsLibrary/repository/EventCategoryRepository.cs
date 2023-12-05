using EventsLibrary.exception;
using EventsLibrary.model;
using System.Collections.Generic;

namespace EventsLibrary.repository
{
    public static class EventCategoryRepository
    {
        private static List<EventCategory> eventCategories = new List<EventCategory>();

        public static void AddCategory(EventCategory category)
        {
            if (category == null)
            {
                throw new InsertEntityException("Category can't be null");
            }
            if (GetCategoryByName(category.Name) != null)
            {
                throw new InsertEntityException("We have category with name: " + category.Name);
            }
            eventCategories.Add(category);
        }

        public static void UpdateCategoryDescription(string name, string newDescription)
        {
            EventCategory category = GetCategoryByName(name);
            if (category == null)
            {
                throw new UpdateEntityException("We haven'y category with name: " + name);
            }
            category.Description = newDescription;
        }

        public static void UpdateCategoryName(string oldName, string newName)
        {
            EventCategory category = GetCategoryByName(oldName);
            if (category == null)
            {
                throw new UpdateEntityException("We haven'y category with name: " + oldName);
            }
            category.Name = newName;
        }

        public static EventCategory GetCategoryByName(string name)
        {
            foreach (var category in eventCategories)
            {
                if (category.Name.Equals(name))
                {
                    return category;
                }
            }
            return null;
        }

        public static bool DeleteCategoryByName(string name)
        {
            EventCategory category = GetCategoryByName(name);
            if (category == null) 
            {
                throw new DeleteEntityException("We don't have category with name: " + name);
            }
            eventCategories.Remove(category);
            return true;
        }

        public static List<EventCategory> GetAll()
        {
            List<EventCategory> copiedCategory = new List<EventCategory> ();
            foreach (var category in eventCategories)
            {
                copiedCategory.Add(category.Copy());
            }
            return copiedCategory;
        }

        public static void ClearAll()
        {
            eventCategories.Clear();
        }
    }
}
