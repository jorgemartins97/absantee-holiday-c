using System.Text.Json;
using Newtonsoft.Json;

namespace Application.DTO
{
    public class AssociationAmqpDTO
    {
        public long Id { get; set; }
        public long ColaboratorId { get; set; }
        public long ProjectId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
<<<<<<< HEAD
        public bool Fundamental { get; set; }
=======
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33


        public AssociationAmqpDTO() { }

<<<<<<< HEAD
        public AssociationAmqpDTO(long id, long colabId, long projectId, DateOnly startDate, DateOnly endDate,bool fundamental)
=======
        public AssociationAmqpDTO(long id, long colabId, long projectId, DateOnly startDate, DateOnly endDate)
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
        {
            Id = id;
            ColaboratorId = colabId;
            ProjectId = projectId;
            StartDate = startDate;
            EndDate = endDate;
<<<<<<< HEAD
            Fundamental = fundamental;
=======
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
        }

        static public string Serialize(AssociationAmqpDTO associationDTO)
        {
            var jsonMessage = JsonConvert.SerializeObject(associationDTO);
            return jsonMessage;
        }

        static public AssociationAmqpDTO Deserialize(string jsonMessage)
        {
            var associationDTO = JsonConvert.DeserializeObject<AssociationAmqpDTO>(jsonMessage);
            return associationDTO!;
        }

    }
}