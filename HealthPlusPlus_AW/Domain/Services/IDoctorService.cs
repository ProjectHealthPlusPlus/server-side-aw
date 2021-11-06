﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HealthPlusPlus_AW.Domain.Models;
using HealthPlusPlus_AW.Services.Communications;

namespace HealthPlusPlus_AW.Domain.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> ListAsync();
        Task<IEnumerable<Doctor>> ListBySpecialtyIdAsync(int specialtyId);
        Task<IEnumerable<Doctor>> ListByClinicIdAsync(int clinicId);
        Task<DoctorResponse> SaveAsync(Doctor doctor);
        Task<DoctorResponse> FindIdAsync(int id);
        Task<DoctorResponse> UpdateAsync(int id, Doctor doctor);
        Task<DoctorResponse> DeleteAsync(int id);
    }
}