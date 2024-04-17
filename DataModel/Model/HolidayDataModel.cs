using DataModel.Mapper;
using DataModel.Model;
using Domain.Model;

public class HolidayDataModel
{
    public long Id { get; set; }
    public long colaboratorId { get; set; }
    public List<HolidayPeriodDataModel> holidayPeriods { get; set; }

    public HolidayDataModel() {}

    public HolidayDataModel(Holiday holiday)
    {
        Id = holiday.Id;

        // Assuming you have a mapper that converts from the domain Colaborator model to the data model
        colaboratorId = holiday.GetColaborator();
        
        // And assuming your holiday periods need to be converted as well
        List<HolidayPeriod> HolidayPeriods = holiday.GetHolidayPeriods();

        holidayPeriods = new List<HolidayPeriodDataModel>();
            foreach(var hp in HolidayPeriods){
                holidayPeriods.Add(new HolidayPeriodDataModel(hp));

            }

        
    }
}
