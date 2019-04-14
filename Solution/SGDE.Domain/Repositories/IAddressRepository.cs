namespace SGDE.Domain.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;

    #endregion

    public interface IAddressRepository : IDisposable
    {
        Task<List<Address>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Address> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<List<Address>> GetByUserIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Address> AddAsync(Address newAddress, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Address address, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        List<Address> GetAll();
        Address GetById(int id);
        List<Address> GetByUserId(int id);
        Address Add(Address newAddress);
        bool Update(Address address);
        bool Delete(int id);
    }
}
