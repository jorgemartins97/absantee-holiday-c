namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

using DataModel.Model;
using DataModel.Mapper;

using Domain.Model;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class HolidayPendingRepository : GenericRepository<Holiday>, IHolidayPendingRepository
{    
    HolidayPendingMapper _holidayPendingMapper;
    ColaboratorsIdMapper _colaboratorsIdMapper;
    public HolidayPendingRepository(AbsanteeContext context, HolidayPendingMapper mapper,ColaboratorsIdMapper colaboratorsIdMapper) : base(context!)
    {
        _holidayPendingMapper = mapper;
        _colaboratorsIdMapper = colaboratorsIdMapper;
    }

    public async Task<IEnumerable<Holiday>> GetHolidaysAsync()
    {
        try {
            IEnumerable<HolidayPendingDataModel> holidaysPendingDataModel = await _context.Set<HolidayPendingDataModel>()
                    .Include(c => c.colaboratorId)
                    .ToListAsync();

            IEnumerable<Holiday> holidays = _holidayPendingMapper.ToDomain(holidaysPendingDataModel);

            return holidays;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Holiday>> GetHolidaysByIdAsync(long id)
    {
        try {
            IEnumerable<HolidayPendingDataModel> holidayPendingDataModel = await _context.Set<HolidayPendingDataModel>()
                    .Include(c => c.colaboratorId)
                    .Where(c => c.Id==id)
                    .ToListAsync();

            IEnumerable<Holiday> holidays = _holidayPendingMapper.ToDomain(holidayPendingDataModel);

            return holidays;
        }
        catch
        {
            throw;
        }
    }
    public async Task<IEnumerable<Holiday>> GetHolidaysByColabIdAsync(long colabId)
    {
        try {
            IEnumerable<HolidayPendingDataModel> holidaysPendingDataModel = await _context.Set<HolidayPendingDataModel>()
                    .Include(c => c.colaboratorId)
                    .Where(c => c.colaboratorId.Id==colabId)
                    .ToListAsync();

            if(holidaysPendingDataModel== null){
                return null;
            }

            IEnumerable<Holiday> holidays = _holidayPendingMapper.ToDomain(holidaysPendingDataModel);

            return holidays;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Holiday> AddHoliday(Holiday holiday)
    {
        try {

            ColaboratorsIdDataModel colaboratorDataModel = await _context.Set<ColaboratorsIdDataModel>()
                .FirstAsync(c => c.Id == holiday.GetColaborator());
            HolidayPendingDataModel holidayPendingDataModel = _holidayPendingMapper.ToDataModel(holiday,colaboratorDataModel);

            EntityEntry<HolidayPendingDataModel> holidayPendingDataModelEntityEntry = _context.Set<HolidayPendingDataModel>().Add(holidayPendingDataModel);
            
            await _context.SaveChangesAsync();

            HolidayPendingDataModel holidayPendingDataModelSaved = holidayPendingDataModelEntityEntry.Entity;

            Holiday holidayPendingSaved = _holidayPendingMapper.ToDomain(holidayPendingDataModelSaved);

            return holidayPendingSaved;    
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> HolidayExists(long id)
    {
        return await _context.Set<HolidayPendingDataModel>().AnyAsync(e => e.Id == id);
    }
}