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
    [Route("/api/package/{trackingNumber}/stops")]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IRepository _repository;

        public StopsController(IRepository repository, ILogger<StopsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get(string trackingNumber)
        {
            try
            {
                var stops = _repository.GetPackageByTracking(trackingNumber);

                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(stops.Stops.OrderBy(s => s.Date).ToList()));
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to find tracking number: {0}", ex);
            }

            return BadRequest("Failed to find tracking number");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("")]
        public async Task<IActionResult> Post(string trackingNumber, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);

                    //save to db
                    _repository.AddStops(trackingNumber, newStop);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{trackingNumber}/stops/{newStop.Location}", Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new stop information: {0}", ex);
            }
            return BadRequest("Failed to save new stop information");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("")]
        public async Task<IActionResult> Delete(string trackingNumber, [FromBody]StopViewModel vm)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _repository.deleteStop(trackingNumber, vm.Location, vm.Activity);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok("Successfully Deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to delete: " + ex);
            }

            return BadRequest("Failed to delete stop");
        }
    }
}
