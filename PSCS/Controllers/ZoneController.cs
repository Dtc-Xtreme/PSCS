﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSCS.API.Models;
using PSCS.Domain;
using PSCS.Infrastructure.Repositories;

namespace PSCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZoneController : Controller
    {
        private readonly IZoneRepository zoneRepository;

        public ZoneController(IZoneRepository repository)
        {
            this.zoneRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<Zone> zones = await zoneRepository.Zones.ToListAsync();
            return Ok(zones == null ? BadRequest() : zones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            Zone? zone = await zoneRepository.FindById(id);

            return Ok(zone == null ? BadRequest() : zone);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ZoneDTO zone)
        {
            Zone? newZone = null;

            if (ModelState.IsValid)
            {
                newZone = new Zone
                {
                    Name = zone.Name
                };
            }
            return Ok(await zoneRepository.Create(newZone) == false ? BadRequest() : newZone);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Zone zone)
        {
            return Ok(await zoneRepository.Update(zone) == false ? BadRequest() : zone);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await zoneRepository.Remove(id) == false ? BadRequest() : "Zone is removed!");
        }
    }
}
