using System;
using System.Collections.Generic;
using ElearningDemo.Models.ValueType;

namespace ELearningDemo.Models.Entities
{
    public class Course
    {
        public Course(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Il titolo deve essere inserito");
            }
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("L'autore deve essere inserito");
            }
            Title = title;
            Author = author;
            Lessons = new HashSet<Lesson>();
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Author { get; private set; } 
        public string Email { get; private set; }  
        public double Rating { get; private set; }
        public Money FullPrice { get; private set; }
        public Money CurrentPrice { get; private set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public void ChangeTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("Il titolo deve essere inserito");
            }
            Title = newTitle;
        }

        public void ChangePrice(Money newFullPrice, Money newCurrentPrice)
        {
            if (newFullPrice == null || newCurrentPrice == null)
            {
                throw new ArgumentException("Il prezzo è obbligatorio");
            }
            if (newFullPrice.Currency != newCurrentPrice.Currency)
            {
                throw new ArgumentException("La valuta non può essere diversa");
            }
            if (newFullPrice.Amount <= newCurrentPrice.Amount)
            {
                throw new ArgumentException("Il prezzo scontato non può essere inferiore o uguale al prezzo pieno");
            }
            FullPrice = newFullPrice;
            CurrentPrice = newCurrentPrice;
        }
    }
}