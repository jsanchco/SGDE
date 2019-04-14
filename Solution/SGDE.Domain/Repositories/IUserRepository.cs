﻿namespace SGDE.Domain.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;

    #endregion

    public interface IUserRepository : IDisposable
    {
        Task<User> Authenticate(string username, string password, CancellationToken ct = default(CancellationToken));
        Task<List<User>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<User> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));        
        Task<User> AddAsync(User newUser, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(User user, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        List<User> GetAll();
        User GetById(int id);
        User Add(User newUser);
        bool Update(User user);
        bool Delete(int id);
    }
}
