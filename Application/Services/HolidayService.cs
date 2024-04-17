namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Microsoft.EntityFrameworkCore;
using DataModel.Repository;
using Domain.IRepository;
using Domain.Factory;
using DataModel.Model;
using Gateway;

public class HolidayService {

    private readonly AbsanteeContext _context;
    private readonly IHolidayRepository _holidayRepository;

    private readonly IColaboratorsIdRepository _colaboratorsIdRepository;
    private readonly IHolidayPeriodFactory _holidayPeriodFactory;
    private readonly HolidayAmpqGateway _holidayAmqpGateway;


    
    public HolidayService(IHolidayRepository holidayRepository, IHolidayPeriodFactory holidayPeriodFactory, HolidayAmpqGateway holidayAmqpGateway,IColaboratorsIdRepository colaboratorsIdRepository) {
        _holidayRepository = holidayRepository;
        _holidayPeriodFactory = holidayPeriodFactory;
        _holidayAmqpGateway=holidayAmqpGateway;
        _colaboratorsIdRepository = colaboratorsIdRepository;

    }

    

    public async Task<HolidayDTO> Add(HolidayDTO holidayDto, List<string> errorMessages)
    {
        bool bExists = await _holidayRepository.HolidayExists(holidayDto.Id);
        bool colabExists = await _colaboratorsIdRepository.ColaboratorExists(holidayDto._colabId);
        if(bExists) {
            errorMessages.Add("Holiday already exists");
            return null;
        }
        if(!colabExists) {
            errorMessages.Add("Colab doesn't exist");
            return null;
        }

        Holiday holiday = HolidayDTO.ToDomain(holidayDto);

        holiday = await _holidayRepository.AddHoliday(holiday);

        HolidayDTO holidayDTO = HolidayDTO.ToDTO(holiday);

        string holidayAmqpDTO = HolidayGatewayDTO.Serialize(holidayDTO);	
        _holidayAmqpGateway.Publish(holidayAmqpDTO);

        return holidayDTO;
    }

    public async Task<bool> AddHolidayPeriod(long id,HolidayPeriodDTO holidayPeriodDTO,List<string> errorMessages)
    {


        Holiday holiday = await _holidayRepository.GetHolidayByIdAsync(id);

        if(holiday!=null)
        {
            HolidayDTO.UpdateToDomain(holidayPeriodDTO.StartDate,holidayPeriodDTO.EndDate,_holidayPeriodFactory,holiday);
            
            holiday = await _holidayRepository.AddHolidayPeriod(holiday, errorMessages);

            HolidayDTO holidayDTO = HolidayDTO.ToDTO(holiday);

            string holidayAmqpDTO = HolidayGatewayDTO.Serialize(holidayDTO);	
            _holidayAmqpGateway.PublishNewHolidayPeriod(holidayAmqpDTO);

            return true;
        }
        else
        {
            errorMessages.Add("Not found");

            return false;
        }
    }

    
}