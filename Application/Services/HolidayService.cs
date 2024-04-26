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
    private readonly IHolidayPendingRepository _holidayPendingRepository;
<<<<<<< HEAD

    private readonly IHolidayRepository _holidayRepository;
=======
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
    private readonly IColaboratorsIdRepository _colaboratorsIdRepository;
    private readonly IHolidayPeriodFactory _holidayPeriodFactory;
    private readonly HolidayAmpqGateway _holidayAmqpGateway;


    
<<<<<<< HEAD
    public HolidayService( IHolidayRepository holidayRepository,IHolidayPendingRepository holidayPendingRepository, IHolidayPeriodFactory holidayPeriodFactory, HolidayAmpqGateway holidayAmqpGateway,IColaboratorsIdRepository colaboratorsIdRepository) {
=======
    public HolidayService(IHolidayPendingRepository holidayPendingRepository, IHolidayPeriodFactory holidayPeriodFactory, HolidayAmpqGateway holidayAmqpGateway,IColaboratorsIdRepository colaboratorsIdRepository) {
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
        _holidayPendingRepository = holidayPendingRepository;
        _holidayPeriodFactory = holidayPeriodFactory;
        _holidayAmqpGateway=holidayAmqpGateway;
        _colaboratorsIdRepository = colaboratorsIdRepository;
        _holidayRepository = holidayRepository;
    }    

    public async Task<HolidayDTO> Add(HolidayDTO holidayDto, List<string> errorMessages)
    {
<<<<<<< HEAD
        Console.WriteLine($"add entered");
        bool bExists = await _holidayRepository.HolidayExists(holidayDto.Id);
=======
        bool bExists = await _holidayPendingRepository.HolidayExists(holidayDto.Id);
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
        bool colabExists = await _colaboratorsIdRepository.ColaboratorExists(holidayDto._colabId);
        if(bExists) {
            errorMessages.Add("Holiday already exists");
            return null;
        }
         Console.WriteLine($"add entered2");
        if(!colabExists) {
            errorMessages.Add("Colab doesn't exist");
            return null;
        }
        Console.WriteLine($"add entered3");
        Holiday holiday = HolidayDTO.ToDomain(holidayDto);

        holiday = await _holidayPendingRepository.AddHoliday(holiday);

        HolidayDTO holidayDTO = HolidayDTO.ToDTO(holiday);

        string holidayAmqpDTO = HolidayGatewayDTO.Serialize(holidayDTO);	
        _holidayAmqpGateway.Publish(holidayAmqpDTO);

        return holidayDTO;
    }

    

    
}