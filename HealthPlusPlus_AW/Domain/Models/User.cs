﻿namespace HealthPlusPlus_AW.Domain.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string Dni { get; set; } 
        public string Name { get; set; } 
        public string Lastname { get; set; }
        public int Age { get; set; }
    }
}