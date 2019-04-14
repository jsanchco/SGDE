// ReSharper disable InconsistentNaming
namespace SGDE.Domain.Supervisor
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Converters;
    using Entities;
    using ViewModels;

    #endregion

    public partial class Supervisor
    {
        public async Task<List<AddressViewModel>> GetAllAddressAsync(CancellationToken ct = default(CancellationToken))
        {
            return AddressConverter.ConvertList(await _addressRepository.GetAllAsync(ct));
        }

        public async Task<AddressViewModel> GetAddressByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var addressViewModel = AddressConverter.Convert(await _addressRepository.GetByIdAsync(id, ct));
            addressViewModel.userName = 
                _userRepository.GetByIdAsync(addressViewModel.userId, ct).Result.Name +
                _userRepository.GetByIdAsync(addressViewModel.userId, ct).Result.Surname;

            return addressViewModel;
        }

        public async Task<List<AddressViewModel>> GetAddressesByUserIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return AddressConverter.ConvertList(await _addressRepository.GetByUserIdAsync(id, ct));
        }

        public async Task<AddressViewModel> AddAddressAsync(AddressViewModel newAddressViewModel, CancellationToken ct = default(CancellationToken))
        {
            var address = new Address
            {
                AddedDate = DateTime.Now,
                ModifiedDate = null,
                IPAddress = newAddressViewModel.iPAddress,

                Street = newAddressViewModel.street,
                Number = newAddressViewModel.number,
                UserId = newAddressViewModel.userId
            };

            await _addressRepository.AddAsync(address, ct);
            return newAddressViewModel;
        }

        public async Task<bool> UpdateAddressAsync(AddressViewModel addressViewModel, CancellationToken ct = default(CancellationToken))
        {
            if (addressViewModel.id == null)
                return false;

            var address = await _addressRepository.GetByIdAsync((int)addressViewModel.id, ct);

            if (address == null) return false;
            
            address.ModifiedDate = DateTime.Now;
            address.IPAddress = addressViewModel.iPAddress;

            address.Street = addressViewModel.street;
            address.Number = addressViewModel.number;
            address.UserId = addressViewModel.userId;

            return await _addressRepository.UpdateAsync(address, ct);
        }

        public async Task<bool> DeleteAddressAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _addressRepository.DeleteAsync(id, ct);
        }

        public List<AddressViewModel> GetAllAddress()
        {
            return AddressConverter.ConvertList(_addressRepository.GetAll());
        }

        public AddressViewModel GetAddressById(int id)
        {
            var addressViewModel = AddressConverter.Convert(_addressRepository.GetById(id));
            addressViewModel.userName =
                _userRepository.GetById(addressViewModel.userId).Name +
                _userRepository.GetById(addressViewModel.userId).Surname;

            return addressViewModel;
        }

        public List<AddressViewModel> GetAddressesByUserId(int id)
        {
            return AddressConverter.ConvertList(_addressRepository.GetByUserId(id));
        }

        public AddressViewModel AddAddress(AddressViewModel newAddressViewModel)
        {
            var address = new Address
            {
                AddedDate = DateTime.Now,
                ModifiedDate = null,
                IPAddress = newAddressViewModel.iPAddress,

                Street = newAddressViewModel.street,
                Number = newAddressViewModel.number,
                UserId = newAddressViewModel.userId
            };

            _addressRepository.Add(address);
            return newAddressViewModel;
        }

        public bool UpdateAddress(AddressViewModel addressViewModel)
        {
            if (addressViewModel.id == null)
                return false;

            var address = _addressRepository.GetById((int)addressViewModel.id);

            if (address == null) return false;

            address.ModifiedDate = DateTime.Now;
            address.IPAddress = addressViewModel.iPAddress;

            address.Street = addressViewModel.street;
            address.Number = addressViewModel.number;
            address.UserId = addressViewModel.userId;

            return _addressRepository.Update(address);
        }

        public bool DeleteAddress(int id)
        {
            return _addressRepository.Delete(id);
        }
    }
}
