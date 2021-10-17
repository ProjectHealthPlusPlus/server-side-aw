﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HealthPlusPlus_AW.Domain.Models;
using HealthPlusPlus_AW.Domain.Repositories;
using HealthPlusPlus_AW.Domain.Services;
using HealthPlusPlus_AW.Resources;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlusPlus_AW.Controllers
{
    [Route("/api/v1/[controller]")]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly IMapper _mapper;

        public SpecialtyController(ISpecialtyService specialtyService, IMapper mapper)
        {
            _specialtyService = specialtyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SpecialtyResource>> GetAllAsync()
        {
            var specialties = await _specialtyService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Specialty>, IEnumerable<SpecialtyResource>>(specialties);
            return resources;
        }
    }
}