using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {   
        private readonly HolidayService _holidayService;

        public HolidayController(HolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        
        // POST: api/Holiday
        [HttpPost]
        public async Task<ActionResult<HolidayDTO>> PostHoliday(HolidayDTO holidayDTO)
        {
            List<string> errorMessages = new List<string>(); // Declare errorMessages here to capture new errors for each call

            HolidayDTO holidayResultDTO = await _holidayService.Add(holidayDTO, errorMessages);
            if (holidayResultDTO != null)
            {
                // Assuming GetHolidayById action expects a route parameter named 'id'
                return Ok(holidayResultDTO);
            }
            else
            {
                return BadRequest(errorMessages);
            }
        }

        // PUT: api/Holiday
        [HttpPut("{id}")]
        public async Task<ActionResult<HolidayDTO>> AddNewHolidayPeriodToHoliday(long id,HolidayPeriodDTO holidayPeriodDTO)
        {
            List<string> errorMessages = new List<string>(); // Declare errorMessages here to capture new errors for each call

            bool wasUpdated = await _holidayService.AddHolidayPeriod(id,holidayPeriodDTO, errorMessages);
            if (!wasUpdated  && errorMessages.Any() )
            {
                return BadRequest(errorMessages);
            }

            return Ok();
        }

        // DELETE: api/Colaborator/5
        // [HttpDelete("{email}")]
        // Uncomment and implement as needed.
    }
}
