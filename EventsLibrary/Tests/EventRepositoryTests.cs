using EventsLibrary.exception;
using EventsLibrary.model;
using EventsLibrary.repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary.Tests
{
    [TestFixture]
    public class EventRepositoryTests
    {
        [Test]
        public void AddEvent_ShouldIncreaseEventCountAndContainNewEvent()
        {
            // Given
            EventsRepository.Clear();
            var newEvent = new Event("New Event", "New Place", new EventCategory("Category", "Description"));

            // When
            EventsRepository.AddEvent(newEvent);

            // Then
            Assert.That(EventsRepository.GetAll().Count, Is.EqualTo(1));
            Assert.That(EventsRepository.GetEventByName("New Event"), Is.Not.Null);
        }

        [Test]
        public void AddEvent_ShouldThrowInsertEntityExceptionWhenEventIsNull()
        {
            // Given
            EventsRepository.Clear();
            Event nullEvent = null;

            // When, Then
            Assert.Throws<InsertEntityException>(() => EventsRepository.AddEvent(nullEvent));
        }

        [Test]
        public void AddEvent_ShouldThrowInsertEntityExceptionWhenEventWithSameNameExists()
        {
            // Given
            EventsRepository.Clear();
            var existingEvent = new Event("Existing Event", "Existing Place", new EventCategory("Category", "Description"));
            EventsRepository.AddEvent(existingEvent);

            var newEventWithSameName = new Event("Existing Event", "New Place", new EventCategory("Category", "Description"));

            // When, Then
            Assert.Throws<InsertEntityException>(() => EventsRepository.AddEvent(newEventWithSameName));
        }

        [Test]
        public void GetEventByName_ShouldReturnCorrectEvent()
        {
            // Given
            EventsRepository.Clear();
            var eventName = "Existing Event";
            var existingEvent = new Event(eventName, "Existing Place", new EventCategory("Category", "Description"));
            EventsRepository.AddEvent(existingEvent);

            // When
            var resultEvent = EventsRepository.GetEventByName(eventName);

            // Then
            Assert.That(resultEvent, Is.Not.Null);
            Assert.That(resultEvent, Is.EqualTo(existingEvent));
        }

        [Test]
        public void AddEventToFavorite_ShouldMarkEventAsFavorite()
        {
            // Given
            EventsRepository.Clear();
            var eventName = "Event to Favorite";
            var eventToAdd = new Event(eventName, "Place", new EventCategory("Category", "Description"));
            EventsRepository.AddEvent(eventToAdd);

            // When
            EventsRepository.AddEventToFavorite(eventName);

            // Then
            var favoriteEvent = EventsRepository.GetEventByName(eventName);
            Assert.That(favoriteEvent.IsFavorite, Is.True);
        }

        [Test]
        public void AddEventToFavorite_ShouldThrowUpdateEntityExceptionWhenEventDoesNotExist()
        {
            // Given
            EventsRepository.Clear();
            var nonExistentEventName = "Non-existent Event";

            // When, Then
            Assert.Throws<UpdateEntityException>(() => EventsRepository.AddEventToFavorite(nonExistentEventName));
        }

        [Test]
        public void DeleteEventByName_ShouldRemoveEventAndReturnTrue()
        {
            // Given
            EventsRepository.Clear();
            var eventNameToDelete = "Event to Delete";
            var eventToDelete = new Event(eventNameToDelete, "Place", new EventCategory("Category", "Description"));
            EventsRepository.AddEvent(eventToDelete);

            // When
            var isDeleted = EventsRepository.DeleteEventByName(eventNameToDelete);

            // Then
            Assert.That(isDeleted, Is.True);
            Assert.That(EventsRepository.GetEventByName(eventNameToDelete), Is.Null);
        }

        [Test]
        public void DeleteEventByName_ShouldThrowDeleteEntityExceptionWhenEventDoesNotExist()
        {
            // Given
            EventsRepository.Clear();
            var nonExistentEventName = "Non-existent Event";

            // When, Then
            Assert.Throws<DeleteEntityException>(() => EventsRepository.DeleteEventByName(nonExistentEventName));
        }

        [Test]
        public void GetFavoriteEvents_ShouldReturnOnlyFavoriteEventsOrderedByCategoryDescending()
        {
            // Given
            EventsRepository.Clear();
            var favoriteEvent1 = new Event("Favorite Event 1", "Place", new EventCategory("Category", "Description"));
            var favoriteEvent2 = new Event("Favorite Event 2", "Place", new EventCategory("Category", "Description"));

            EventsRepository.AddEvent(favoriteEvent1);
            EventsRepository.AddEvent(favoriteEvent2);
            EventsRepository.AddEventToFavorite("Favorite Event 2");

            // When
            var favoriteEvents = EventsRepository.GetFavoriteEvents();

            // Then
            Assert.That(favoriteEvents.Count, Is.EqualTo(1));
            Assert.That(favoriteEvents[0].Name, Is.EqualTo("Favorite Event 2"));
        }

        [Test]
        public void GetEventsByCategory_ShouldReturnEventsInCategoryOrderedByName()
        {
            // Given
            EventsRepository.Clear();
            var categoryName = "Category";
            var eventInCategory1 = new Event("Event 1", "Place", new EventCategory(categoryName, "Description"));
            var eventInCategory2 = new Event("Event 2", "Place", new EventCategory(categoryName, "Description"));
            var eventInOtherCategory = new Event("Other Event", "Place", new EventCategory("Other Category", "Description"));

            EventsRepository.AddEvent(eventInCategory1);
            EventsRepository.AddEvent(eventInCategory2);
            EventsRepository.AddEvent(eventInOtherCategory);

            // When
            var eventsInCategory = EventsRepository.GetEventsByCategory(categoryName);

            // Then
            Assert.That(eventsInCategory.Count, Is.EqualTo(2));
            Assert.That(eventsInCategory[0], Is.EqualTo(eventInCategory1));
            Assert.That(eventsInCategory[1], Is.EqualTo(eventInCategory2));
        }
    }
}
