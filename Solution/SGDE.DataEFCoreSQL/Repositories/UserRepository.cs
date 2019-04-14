namespace SGDE.DataEFCoreSQL.Repositories
{
    #region Using

    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    #endregion

    public class UserRepository : IUserRepository
    {
        private readonly EFContext _context;

        public UserRepository(EFContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private async Task<bool> UserExistsAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        private bool UserExists(int id)
        {
            return GetById(id) != null;
        }

        public async Task<User> Authenticate(string username, string password, CancellationToken ct = default(CancellationToken))
        {
            return await _context.User
                .Include(x => x.Profession)
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == password, ct);
        }

        public async Task<List<User>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.User
                .Include(x => x.Profession)
                .ToListAsync(ct);
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.User
                .Include(x => x.Profession)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<User> AddAsync(User newUser, CancellationToken ct = default(CancellationToken))
        {
            _context.User.Add(newUser);
            await _context.SaveChangesAsync(ct);
            return newUser;
        }

        public async Task<bool> UpdateAsync(User user, CancellationToken ct = default(CancellationToken))
        {
            if (!await UserExistsAsync(user.Id, ct))
                return false;

            _context.User.Update(user);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await UserExistsAsync(id, ct))
                return false;

            var toRemove = _context.User.Find(id);
            _context.User.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public List<User> GetAll()
        {
            return  _context.User
                .Include(x => x.Profession)
                .ToList();
        }

        public User GetById(int id)
        {
            return _context.User
                .Include(x => x.Profession)
                .FirstOrDefault(x => x.Id == id);
        }

        public User Add(User newUser)
        {
            _context.User.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public bool Update(User user)
        {
            if (!UserExists(user.Id))
                return false;

            _context.User.Update(user);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (UserExists(id))
                return false;

            var toRemove = _context.User.Find(id);
            _context.User.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }
    }
}
