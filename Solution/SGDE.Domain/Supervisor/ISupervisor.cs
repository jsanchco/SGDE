// ReSharper disable InconsistentNaming
namespace SGDE.Domain.Supervisor
{
    #region Using

    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels;

    #endregion

    public interface ISupervisor
    {
        #region Address

        Task<List<AddressViewModel>> GetAllAddressAsync(CancellationToken ct = default(CancellationToken));
        Task<AddressViewModel> GetAddressByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<List<AddressViewModel>> GetAddressesByUserIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<AddressViewModel> AddAddressAsync(AddressViewModel newAddressViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAddressAsync(AddressViewModel addressViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAddressAsync(int id, CancellationToken ct = default(CancellationToken));

        List<AddressViewModel> GetAllAddress();
        AddressViewModel GetAddressById(int id);
        List<AddressViewModel> GetAddressesByUserId(int id);
        AddressViewModel AddAddress(AddressViewModel newAddressViewModel);
        bool UpdateAddress(AddressViewModel addressViewModel);
        bool DeleteAddress(int id);

        #endregion

        #region User

        Task<UserViewModel> Authenticate(string user, string password, CancellationToken ct = default(CancellationToken));
        Task<List<UserViewModel>> GetAllUserAsync(CancellationToken ct = default(CancellationToken));
        Task<UserViewModel> GetUserByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<UserViewModel> AddUserAsync(UserViewModel newUserViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateUserAsync(UserViewModel userViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteUserAsync(int id, CancellationToken ct = default(CancellationToken));

        List<UserViewModel> GetAllUser();
        UserViewModel GetUserById(int id);
        UserViewModel AddUser(UserViewModel newUserViewModel);
        bool UpdateUser(UserViewModel userViewModel);
        bool DeleteUser(int id);

        #endregion

        #region Profession

        Task<List<ProfessionViewModel>> GetAllProfessionAsync(CancellationToken ct = default(CancellationToken));
        Task<ProfessionViewModel> GetProfessionByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<List<UserViewModel>> GetUsersByProfessionIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<ProfessionViewModel> AddAsync(ProfessionViewModel newProfessionViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(ProfessionViewModel professionViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));

        List<ProfessionViewModel> GetAllProfession();
        ProfessionViewModel GetProfessionById(int id);
        List<UserViewModel> GetUsersByProfessionId(int id);
        ProfessionViewModel AddProfession(ProfessionViewModel newProfessionViewModel);
        bool UpdateProfession(ProfessionViewModel professionViewModel);
        bool DeleteProfession(int id);

        #endregion
    }
}
