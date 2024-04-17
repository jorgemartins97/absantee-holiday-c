using Microsoft.AspNetCore.Mvc;

using Application.Services;
using Application.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayPeriodController : ControllerBase
    {   
        private readonly HolidayPeriodService _holidayPeriodService;

        List<string> _errorMessages = new List<string>();

        public HolidayPeriodController(HolidayPeriodService holidayPeriodService)
        {
            _holidayPeriodService = holidayPeriodService;
        }

        // POST: api/Holiday
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HolidayPeriodDTO>> PostHolidayPeriod(HolidayPeriodDTO holidayPeriodDTO)
        {
            HolidayPeriodDTO holidayPeriodResultDTO = await _holidayPeriodService.Add(holidayPeriodDTO, _errorMessages);

            if(holidayPeriodResultDTO != null)
                return Ok(holidayPeriodResultDTO);
            else
                return BadRequest(_errorMessages);
        }

    }
}
