namespace SGDE.Domain.Converters
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using ViewModels;

    #endregion

    public static class AddressConverter
    {
        public static AddressViewModel Convert(Address address)
        {
            if (address == null)
                return null;

            var addressViewModel = new AddressViewModel
            {
                id = address.Id,
                addedDate = address.AddedDate,
                modifiedDate = address.ModifiedDate,
                iPAddress = address.IPAddress,
                
                street = address.Street,
                number = address.Number,
                userId = address.UserId
            };

            return addressViewModel;
        }

        public static List<AddressViewModel> ConvertList(IEnumerable<Address> addresses)
        {
            return addresses?.Select(user =>
                {
                    var model = new AddressViewModel
                    {
                        id = user.Id,
                        addedDate = user.AddedDate,
                        modifiedDate = user.ModifiedDate,
                        iPAddress = user.IPAddress,

                        street = user.Street,
                        number = user.Number,
                        userId = user.UserId
                    };
                    return model;
                })
                .ToList();
        }
    }
}
