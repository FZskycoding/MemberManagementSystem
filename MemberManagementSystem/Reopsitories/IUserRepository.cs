using MemberManagementSystem.Models;

namespace MemberManagementSystem.Reopsitories
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();

        void Add(User user);
        void Update(User user);
        void Delete(int id);
        void Save();
    }
}
