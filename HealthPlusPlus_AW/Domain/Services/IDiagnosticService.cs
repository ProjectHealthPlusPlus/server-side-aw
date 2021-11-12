﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HealthPlusPlus_AW.Domain.Models;
using HealthPlusPlus_AW.Services.Communications;

namespace HealthPlusPlus_AW.Domain.Services
{
    public interface IDiagnosticService
    {
        Task<IEnumerable<Diagnostic>> ListAsync();
        Task<IEnumerable<Diagnostic>> ListBySpecialtyIdAsync(int specialtyId);
        Task<IEnumerable<Diagnostic>> ListByMedicalHistoryIdAsync(int medicalHistoryId);
        Task<DiagnosticResponse> SaveAsync(Diagnostic diagnostic);
        Task<DiagnosticResponse> FindIdAsync(int id);
        Task<DiagnosticResponse> UpdateAsync(int id, Diagnostic diagnostic);
        Task<DiagnosticResponse> DeleteAsync(int id);
    }
}