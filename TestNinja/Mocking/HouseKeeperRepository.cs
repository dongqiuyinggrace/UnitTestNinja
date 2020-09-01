using System.Linq;

namespace TestNinja.Mocking
{
    public interface IHouseKeeperRepository
    {
        IQueryable<Housekeeper> GetHousekeepers();
    }

    public class HouseKeeperRepository : IHouseKeeperRepository
    {
        public IQueryable<Housekeeper> GetHousekeepers()
        {
            var unitOfWork = new UnitOfWork();
            var housekeepers = unitOfWork.Query<Housekeeper>();
            return housekeepers;
        }

    }
}