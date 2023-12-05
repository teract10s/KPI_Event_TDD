using EventsLibrary.exception;
using EventsLibrary.model;
using EventsLibrary.repository;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;

namespace EventsLibrary.tests
{
    [TestFixture]
    public class EventCategoryRepositoryTests
    {
        [Test]
        public void AddCategory_ValidCategory_AddsToRepository()
        {
            // Given
            EventCategoryRepository.ClearAll();
            EventCategory category = new EventCategory("TestCategory", "TestDescription");

            // When
            EventCategoryRepository.AddCategory(category);

            // Then
            CollectionAssert.Contains(EventCategoryRepository.GetAll(), category);
        }

        [Test]
        public void AddCategory_NullCategory_ThrowsInsertEntityException()
        {
            // Given
            EventCategoryRepository.ClearAll();

            // When, Then
            Assert.Throws<InsertEntityException>(() => EventCategoryRepository.AddCategory(null));
        }

        [Test]
        public void AddCategory_DuplicateCategory_ThrowsInsertEntityException()
        {
            // Given
            EventCategoryRepository.ClearAll();
            EventCategory category = new EventCategory("TestCategory", "TestDescription");
            EventCategoryRepository.AddCategory(category);

            // When, Then
            Assert.Throws<InsertEntityException>(() => EventCategoryRepository.AddCategory(category));
        }

        [Test]
        public void UpdateCategoryDescription_ExistingCategory_UpdatesDescription()
        {
            // Given
            EventCategoryRepository.ClearAll();
            string categoryName = "TestCategory";
            EventCategoryRepository.AddCategory(new EventCategory(categoryName, "OldDescription"));
            string newDescription = "NewDescription";

            // When
            EventCategoryRepository.UpdateCategoryDescription(categoryName, newDescription);

            // Then
            EventCategory updatedCategory = EventCategoryRepository.GetCategoryByName(categoryName);
            Assert.That(newDescription, Is.EqualTo(updatedCategory.Description));
        }

        [Test]
        public void UpdateCategoryDescription_NonexistentCategory_ThrowsUpdateEntityException()
        {
            // Given
            EventCategoryRepository.ClearAll();

            // When, Then
            Assert.Throws<UpdateEntityException>(() => EventCategoryRepository.UpdateCategoryDescription("NonexistentCategory", "NewDescription"));
        }

        [Test]
        public void UpdateCategoryName_ExistingCategory_UpdatesDescription()
        {
            // Given
            EventCategoryRepository.ClearAll();
            string categoryDescription = "TestCategoryDescription";
            EventCategoryRepository.AddCategory(new EventCategory("OldCategoryName", categoryDescription));
            string newCategoryName = "NewCategoryName";

            // When
            EventCategoryRepository.UpdateCategoryName("OldCategoryName", newCategoryName);

            // Then
            EventCategory updatedCategory = EventCategoryRepository.GetCategoryByName(newCategoryName);
            Assert.That(newCategoryName, Is.EqualTo(updatedCategory.Name));
        }

        [Test]
        public void UpdateCategoryName_NonexistentCategory_ThrowsUpdateEntityException()
        {
            // Given
            EventCategoryRepository.ClearAll();

            // When, Then
            Assert.Throws<UpdateEntityException>(() => EventCategoryRepository.UpdateCategoryName("NonexistentCategory", "NewName"));
        }

        [Test]
        public void DeleteCategoryByName_ExistingCategory_RemovesFromRepository()
        {
            // Given
            EventCategoryRepository.ClearAll();
            string categoryName = "TestCategory";
            EventCategoryRepository.AddCategory(new EventCategory(categoryName, "TestDescription"));

            // When
            bool result = EventCategoryRepository.DeleteCategoryByName(categoryName);

            // Then
            Assert.That(result, Is.True);
            Assert.That(EventCategoryRepository.GetCategoryByName(categoryName), Is.Null);
        }

        [Test]
        public void DeleteCategoryByName_NonexistentCategory_ThrowsDeleteEntityException()
        {
            // Given
            EventCategoryRepository.ClearAll();

            // When, Then
            Assert.Throws<DeleteEntityException>(() => EventCategoryRepository.DeleteCategoryByName("NonexistentCategory"));
        }

        [Test]
        public void DeleteCategoryByName_NullCategoryName_ThrowsArgumentNullException()
        {
            // Given
            EventCategoryRepository.ClearAll();

            // When, Then
            Assert.Throws<DeleteEntityException>(() => EventCategoryRepository.DeleteCategoryByName(null));
        }

        [Test]
        public void DeleteCategoryByName_EmptyCategoryName_ThrowsArgumentException()
        {
            // Given
            EventCategoryRepository.ClearAll();
            
            // When, Then
            Assert.Throws<DeleteEntityException>(() => EventCategoryRepository.DeleteCategoryByName(""));
        }

        [Test]
        public void DeleteCategoryByName_CategoryNameWithSpaces_ThrowsArgumentException()
        {
            // Given
            EventCategoryRepository.ClearAll();

            // When, Then
            Assert.Throws<DeleteEntityException>(() => EventCategoryRepository.DeleteCategoryByName("Category Name"));
        }

        [Test]
        public void DeleteCategoryByName_MultipleCategories_RemovesCorrectCategory()
        {
            // Given
            EventCategoryRepository.ClearAll();
            EventCategoryRepository.AddCategory(new EventCategory("Category1", "Description1"));
            EventCategoryRepository.AddCategory(new EventCategory("Category2", "Description2"));
            EventCategoryRepository.AddCategory(new EventCategory("Category3", "Description3"));

            // When
            bool result = EventCategoryRepository.DeleteCategoryByName("Category2");

            // Then
            Assert.That(result, Is.True);
            Assert.That(EventCategoryRepository.GetCategoryByName("Category2"), Is.Null);
            Assert.That(EventCategoryRepository.GetAll().Count, Is.EqualTo(2));
        }

        [Test]
        public void GetAll_ReturnsAllCategories()
        {
            // Given
            EventCategoryRepository.ClearAll();
            EventCategoryRepository.AddCategory(new EventCategory("Category1", "Description1"));
            EventCategoryRepository.AddCategory(new EventCategory("Category2", "Description2"));
            EventCategoryRepository.AddCategory(new EventCategory("Category3", "Description3"));

            // When
            List<EventCategory> allCategories = EventCategoryRepository.GetAll();

            // Then
            Assert.That(allCategories.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetAll_ReturnsSafeCopyOfCategories()
        {
            // Given
            EventCategoryRepository.ClearAll();
            EventCategoryRepository.AddCategory(new EventCategory("Category1", "Description1"));
            EventCategoryRepository.AddCategory(new EventCategory("Category2", "Description2"));

            // When
            List<EventCategory> allCategories = EventCategoryRepository.GetAll();
            EventCategoryRepository.AddCategory(new EventCategory("Category3", "Description3"));

            // Then
            Assert.That(allCategories.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetAll_ReturnsEmptyListWhenNoCategories()
        {
            // Given
            EventCategoryRepository.ClearAll();

            // When
            List<EventCategory> allCategories = EventCategoryRepository.GetAll();

            // Then
            Assert.That(allCategories, Is.Empty);
        }

        [Test]
        public void ClearAll_EmptyList_NoExceptionThrown()
        {
            // Given, When, Then
            Assert.DoesNotThrow(() => EventCategoryRepository.ClearAll());
        }

        [Test]
        public void ClearAll_ListWithCategories_ClearsList()
        {
            // Given
            EventCategoryRepository.AddCategory(new EventCategory("Category1", "Description1"));
            EventCategoryRepository.AddCategory(new EventCategory("Category2", "Description2"));

            // When
            EventCategoryRepository.ClearAll();

            // Then
            Assert.That(EventCategoryRepository.GetAll(), Is.Empty);
        }

        [Test]
        public void ClearAll_ListWithCategories_ReturnsEmptyListAfterClear()
        {
            // Given
            EventCategoryRepository.AddCategory(new EventCategory("Category1", "Description1"));
            EventCategoryRepository.AddCategory(new EventCategory("Category2", "Description2"));

            // When
            EventCategoryRepository.ClearAll();
            List<EventCategory> allCategories = EventCategoryRepository.GetAll();

            // Then
            Assert.That(allCategories, Is.Empty);
        }

        [Test]
        public void ClearAll_MultipleCalls_ClearsListEveryTime()
        {
            // Given
            EventCategoryRepository.AddCategory(new EventCategory("Category1", "Description1"));
            EventCategoryRepository.AddCategory(new EventCategory("Category2", "Description2"));

            // When
            EventCategoryRepository.ClearAll();
            EventCategoryRepository.ClearAll();
            List<EventCategory> allCategories = EventCategoryRepository.GetAll();

            // Then
            Assert.That(allCategories, Is.Empty);
        }
    }
}
