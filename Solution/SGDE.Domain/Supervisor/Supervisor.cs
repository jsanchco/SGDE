// ReSharper disable InconsistentNaming
namespace SGDE.Domain.Supervisor
{
    #region Using

    using Microsoft.Extensions.Options;
    using Helpers;
    using Repositories;

    #endregion

    public partial class Supervisor : ISupervisor
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IProfessionRepository _professionRepository;
        private readonly AppSettings _appSettings;

        public Supervisor()
        {
        }

        public Supervisor(
            IUserRepository userRepository,
            IAddressRepository addressRepository,
            IProfessionRepository professionRepository,
            IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _professionRepository = professionRepository;
            _appSettings = appSettings.Value;
        }
    }
}
