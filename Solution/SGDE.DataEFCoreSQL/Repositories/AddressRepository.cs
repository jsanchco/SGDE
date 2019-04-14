namespace SGDE.DataEFCoreSQL.Repositories
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class AddressRepository : IAddressRepository
    {
        private readonly EFContext _context;

        public AddressRepository(EFContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private async Task<bool> AddressExistsAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        private bool AddressExists(int id)
        {
            return GetById(id) != null;
        }

        public async Task<List<Address>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Address
                .Include(x => x.User)
                .ToListAsync(ct);
        }

        public async Task<Address> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Address
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Address>> GetByUserIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Address
                .Include(x => x.User)
                .Where(x => x.UserId == id)
                .ToListAsync(ct);
        }

        public async Task<Address> AddAsync(Address newAddress, CancellationToken ct = default(CancellationToken))
        {
            _context.Address.Add(newAddress);
            await _context.SaveChangesAsync(ct);
            return newAddress;
        }

        public async Task<bool> UpdateAsync(Address address, CancellationToken ct = default(CancellationToken))
        {
            if (!await AddressExistsAsync(address.Id, ct))
                return false;

            _context.Address.Update(address);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await AddressExistsAsync(id, ct))
                return false;

            var toRemove = _context.Address.Find(id);
            _context.Address.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public List<Address> GetAll()
        {
            return _context.Address
                .Include(x => x.User)
                .ToList();
        }

        public Address GetById(int id)
        {
            return _context.Address
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Address> GetByUserId(int id)
        {
            return _context.Address
                .Include(x => x.User)
                .Where(x => x.UserId == id)
                .ToList();
        }

        public Address Add(Address newAddress)
        {
            _context.Address.Add(newAddress);
            _context.SaveChanges();
            return newAddress;
        }

        public bool Update(Address address)
        {
            if (!AddressExists(address.Id))
                return false;

            _context.Address.Update(address);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!AddressExists(id))
                return false;

            var toRemove = _context.Address.Find(id);
            _context.Address.Remove(toRemove);
            _context.SaveChanges();
            return true;

        }
    }
}
