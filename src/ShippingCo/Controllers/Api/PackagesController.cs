using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShippingCo.Models;
using ShippingCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Controllers.Api
{
    [Authorize(Roles="Admin")]
    [Route("api/package/")]
    public class PackagesController : Controller
    {
        private ILogger<PackagesController> _logger;
        private IRepository _repository;

        public PackagesController(IRepository repository, ILogger<PackagesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var result = _repository.GetAllPackage();
                return Json(Mapper.Map<IEnumerable<PackageViewModel>>(result));
            }
            catch(Exception ex)
            {
                //TODO logging
                _logger.LogError("Unable to fetch all package info: {0}", ex);
                return BadRequest("Error occured." + ex);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]PackageViewModel aPackage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPackage = Mapper.Map<Package>(aPackage);

                    if (_repository.AddPackage(newPackage))
                    {
                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"api/package/{aPackage.TrackingNumber}", Mapper.Map<PackageViewModel>(newPackage));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Failed to add trip with exception:" + ex);

            }
            return BadRequest("Tracking number already exists.");
        }

        [HttpDelete("{trackingNumber}")]
        public async Task<IActionResult> Delete(string trackingNumber)
        {
            try
            {
                _repository.deletePackage(trackingNumber);
                if(await _repository.SaveChangesAsync())
                {
                    return Ok("Successfully Deleted");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to delete: " + ex);
            }

            return BadRequest("Fail to delete tracking information");
        }

    }
}
