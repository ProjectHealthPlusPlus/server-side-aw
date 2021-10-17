﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HealthPlusPlus_AW.Domain.Models;

namespace HealthPlusPlus_AW.Domain.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> ListAsync();
    }
}