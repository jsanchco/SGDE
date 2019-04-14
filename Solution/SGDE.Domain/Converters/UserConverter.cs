namespace SGDE.Domain.Converters
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using ViewModels;

    #endregion

    public static class UserConverter
    {
        public static UserViewModel Convert(User user)
        {
            if (user == null)
                return null;

            var userViewModel = new UserViewModel
            {
                id = user.Id,
                addedDate = user.AddedDate,                                           
                modifiedDate = user.ModifiedDate,
                iPAddress = user.IPAddress,
                name = user.Name,
                surname = user.Surname,
                username = user.Username,
                age = user.Age,
                birthDate = user.BirthDate,
                email = user.Email,
                professionId = user.ProfessionId,
                professionName = user.Profession.Name
            };

            return userViewModel;
        }

        public static List<UserViewModel> ConvertList(IEnumerable<User> users)
        {
            return users?.Select(user =>
                {
                    var model = new UserViewModel
                    {
                        id = user.Id,
                        addedDate = user.AddedDate,
                        modifiedDate = user.ModifiedDate,
                        iPAddress = user.IPAddress,
                        name = user.Name,
                        surname = user.Surname,
                        username = user.Username,
                        age = user.Age,
                        birthDate = user.BirthDate,
                        email = user.Email,
                        professionId = user.ProfessionId,
                        professionName = user.Profession.Name
                    };
                    return model;
                })
                .ToList();
        }
    }
}
