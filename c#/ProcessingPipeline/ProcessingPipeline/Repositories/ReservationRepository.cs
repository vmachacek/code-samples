using ProcessingPipeline.Domain;
using System.Threading.Tasks;

namespace ProcessingPipeline.Repositories
{
    public class ReservationRepository
    {
        //upsert to db
        internal Task UpdateOrCreateAsync(Reservation reservation)
        {
            return Task.CompletedTask;
        }
    }
}
