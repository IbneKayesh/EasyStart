using BS.DMO.Models.Inventory;

namespace BS.Infra.IRepository
{
    public interface IService
    {
        EQResult Insert<T>(T obj, string userId);
        List<T> GetAll<T>();
        List<T> GetAllActive<T>();
        T GetById<T>();
        EQResult Delete(string id);
    }
}
