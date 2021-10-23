﻿using System;

namespace HealthPlusPlus_AW.Resources
{
    public class SaveDiagnosticResource
    {
        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        
        public SpecialtyResource SpecialtyResource { get; set; }
    }
}