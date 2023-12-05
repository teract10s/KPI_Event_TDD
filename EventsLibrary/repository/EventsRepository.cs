using EventsLibrary.exception;
using EventsLibrary.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace EventsLibrary.repository
{
    public static class EventsRepository
    {
        private static List<Event> events = new List<Event>();

        public static void AddEvent(Event eevent) {

            if (eevent == null)
            {
                throw new InsertEntityException("Event can't be null");
            }
            if (GetEventByName(eevent.Name) != null)
            {
                throw new InsertEntityException("We have event with name: " + eevent.Name);
            }
            events.Add(eevent);
        }

        public static Event GetEventByName(string name) 
        {
            foreach (var eevent in events)
            {
                if (eevent.Name.Equals(name))
                {
                    return eevent;
                }
            }
            return null;
        }

        public static void AddEventToFavorite(string name)
        {
            Event eevent = GetEventByName(name);
            if (eevent == null)
            {
                throw new UpdateEntityException("We haven't event with name: " + name);
            }
            eevent.IsFavorite = true;
        }

        public static bool DeleteEventByName(string name)
        {
            Event eevent = GetEventByName(name);
            if (eevent == null)
            {
                throw new DeleteEntityException("We don't have event with name: " + name);
            }
            events.Remove(eevent);
            return true;
        }

        public static List<Event> GetFavoriteEvents()
        {
            var result = new List<Event>();
            foreach (var eevent in events)
                if (eevent.IsFavorite)
                    result.Add(eevent.Copy());

            return result.OrderByDescending(e => e.Category.Name).ToList();
        }

        public static List<Event> GetEventsByCategory(string categoryName)
        {
            var result = new List<Event>();
            foreach (var eevent in events)
                if (eevent.Category.Name.Equals(categoryName))
                    result.Add(eevent.Copy());

            return result.OrderBy(e => e.Name).ToList();
        }
        
        public static List<Event> GetAll()
        {
            List<Event> copiedEvents = new List<Event>();
            foreach (var eevent in events)
            {
                copiedEvents.Add(eevent.Copy());
            }
            return copiedEvents;
        }

        public static void Clear()
        {
            events.Clear();
        }
    }
}
