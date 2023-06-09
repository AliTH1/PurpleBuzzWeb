﻿namespace PurpleBuzzWeb.ViewModel
{
    public class BasketItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public int ServiceCount { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
    }
}
