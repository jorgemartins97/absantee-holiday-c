namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Microsoft.EntityFrameworkCore;
using DataModel.Repository;
using Domain.IRepository;
using Domain.Factory;
using DataModel.Model;
using Gateway;

public class HolidayPendingService {

    private readonly AbsanteeContext _context;
    private readonly IHolidayPendingRepository _holidayPendingRepository;
    private readonly IColaboratorsIdRepository _colaboratorsIdRepository;
    private readonly IHolidayPeriodFactory _holidayPeriodFactory;
    private readonly HolidayPendentAmqpGateway _holidayPendentAmqpGateway;


    
    public HolidayPendingService(IHolidayPendingRepository holidayPendingRepository, IHolidayPeriodFactory holidayPeriodFactory, HolidayPendentAmqpGateway holidayPendentAmqpGateway,IColaboratorsIdRepository colaboratorsIdRepository) {
        _holidayPendingRepository = holidayPendingRepository;
        _holidayPeriodFactory = holidayPeriodFactory;
        _holidayPendentAmqpGateway=holidayPendentAmqpGateway;
        _colaboratorsIdRepository = colaboratorsIdRepository;

    }    

    public async Task<HolidayDTO> Add(HolidayDTO holidayDto, List<string> errorMessages)
    {
        bool bExists = await _holidayPendingRepository.HolidayExists(holidayDto.Id);
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

        holiday = await _holidayPendingRepository.AddHoliday(holiday);

        HolidayDTO holidayDTO = HolidayDTO.ToDTO(holiday);

        string holidayAmqpDTO = HolidayGatewayDTO.Serialize(holidayDTO);	
        _holidayPendentAmqpGateway.PublishNewHolidayPending(holidayAmqpDTO);

        return holidayDTO;
    }

    

    
}