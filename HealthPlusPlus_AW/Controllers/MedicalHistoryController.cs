﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HealthPlusPlus_AW.Domain.Models;
using HealthPlusPlus_AW.Domain.Services;
using HealthPlusPlus_AW.Extensions;
using HealthPlusPlus_AW.Resources;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlusPlus_AW.Controllers
{
    [Route("/api/v1/[controller]")]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMedicalHistoryService _medicalHistoryService;
        private readonly IMapper _mapper;

        public MedicalHistoryController(IMedicalHistoryService medicalHistoryService, IMapper mapper)
        {
            _medicalHistoryService = medicalHistoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MedicalHistoryResource>> GetAllAsync()
        {
            var medicalHistories = await _medicalHistoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<MedicalHistory>, IEnumerable<MedicalHistoryResource>>(medicalHistories);
            return resources;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveMedicalHistoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var medicalHistory = _mapper.Map<SaveMedicalHistoryResource, MedicalHistory>(resource);
            var result = await _medicalHistoryService.SaveAsync(medicalHistory);
            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<MedicalHistory, MedicalHistoryResource>(result.MedicalHistory);
            return Ok(categoryResource);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMedicalHistoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var medicalHistory = _mapper.Map<SaveMedicalHistoryResource, MedicalHistory>(resource);
            var result = await _medicalHistoryService.UpdateAsync(id, medicalHistory);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var medicalHistoryResource = _mapper.Map<MedicalHistory, MedicalHistoryResource>(result.MedicalHistory);
            return Ok(medicalHistoryResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            var result = await _medicalHistoryService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var medicalHistoryResource = _mapper.Map<MedicalHistory, MedicalHistoryResource>(result.MedicalHistory);
            return Ok(medicalHistoryResource); 
        }
    }
}