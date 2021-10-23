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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorResource>> GetAllAsync()
        {
            var doctors = await _doctorService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorResource>>(doctors);
            return resources;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _doctorService.FindIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var doctorResource = _mapper.Map<Doctor, DoctorResource>(result.Doctor);
            return Ok(doctorResource);    
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveDoctorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var doctor = _mapper.Map<SaveDoctorResource, Doctor>(resource);
            var result = await _doctorService.SaveAsync(doctor);
            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Doctor, DoctorResource>(result.Doctor);
            return Ok(categoryResource);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDoctorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var doctor = _mapper.Map<SaveDoctorResource, Doctor>(resource);
            var result = await _doctorService.UpdateAsync(id, doctor);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var categoryResource = _mapper.Map<Doctor, DoctorResource>(result.Doctor);
            return Ok(categoryResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            var result = await _doctorService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var doctorResource = _mapper.Map<Doctor, DoctorResource>(result.Doctor);
            return Ok(doctorResource);    
        }
    }
}