// ReSharper disable InconsistentNaming
namespace SGDE.Domain.Supervisor
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    using Converters;
    using Entities;
    using ViewModels;

    #endregion

    public partial class Supervisor
    {
        public async Task<UserViewModel> Authenticate(string username, string password, CancellationToken ct = default(CancellationToken))
        {
            var userViewModel = UserConverter.Convert(await _userRepository.Authenticate(username, password, ct));
            if (userViewModel == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userViewModel.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                //Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            userViewModel.token = tokenHandler.WriteToken(token);

            return userViewModel;
        }

        public async Task<List<UserViewModel>> GetAllUserAsync(CancellationToken ct = default(CancellationToken))
        {
            return UserConverter.ConvertList(await _userRepository.GetAllAsync(ct));
        }

        public async Task<UserViewModel> GetUserByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var userViewModel = UserConverter.Convert(await _userRepository.GetByIdAsync(id, ct));
            return userViewModel;
        }

        public async Task<UserViewModel> AddUserAsync(UserViewModel newUserViewModel, CancellationToken ct = default(CancellationToken))
        {
            var user = new User
            {
                AddedDate = DateTime.Now,
                ModifiedDate = null,
                IPAddress = newUserViewModel.iPAddress,

                Name = newUserViewModel.name,
                Surname = newUserViewModel.surname,
                Username = newUserViewModel.username,
                Age = newUserViewModel.age,
                BirthDate = newUserViewModel.birthDate,
                Email = newUserViewModel.email,
                Password = "123456",
                ProfessionId = newUserViewModel.professionId
            };

            await _userRepository.AddAsync(user, ct);

            return newUserViewModel;
        }

        public async Task<bool> UpdateUserAsync(UserViewModel userViewModel, CancellationToken ct = default(CancellationToken))
        {
            if (userViewModel.id == null)
                return false;

            var user = await _userRepository.GetByIdAsync((int)userViewModel.id, ct);

            if (user == null) return false;

            user.ModifiedDate = DateTime.Now;
            user.IPAddress = userViewModel.iPAddress;

            user.Name = userViewModel.name;
            user.Surname = userViewModel.surname;
            user.Username = userViewModel.username;
            user.Age = userViewModel.age;
            user.BirthDate = userViewModel.birthDate;
            user.Email = userViewModel.email;            
            user.ProfessionId = userViewModel.professionId;

            return await _userRepository.UpdateAsync(user, ct);
        }

        public async Task<bool> DeleteUserAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.DeleteAsync(id, ct);
        }

        public List<UserViewModel> GetAllUser()
        {
            return UserConverter.ConvertList(_userRepository.GetAll());
        }

        public UserViewModel GetUserById(int id)
        {
            var userViewModel = UserConverter.Convert(_userRepository.GetById(id));
            return userViewModel;
        }

        public UserViewModel AddUser(UserViewModel newUserViewModel)
        {
            var user = new User
            {
                AddedDate = DateTime.Now,
                ModifiedDate = null,
                IPAddress = newUserViewModel.iPAddress,

                Name = newUserViewModel.name,
                Surname = newUserViewModel.surname,
                Username = newUserViewModel.username,
                Age = newUserViewModel.age,
                BirthDate = newUserViewModel.birthDate,
                Email = newUserViewModel.email,
                Password = "123456",
                ProfessionId = newUserViewModel.professionId
            };

            _userRepository.Add(user);

            return newUserViewModel;
        }

        public bool UpdateUser(UserViewModel userViewModel)
        {
            if (userViewModel.id == null)
                return false;

            var user = _userRepository.GetById((int)userViewModel.id);

            if (user == null) return false;

            user.ModifiedDate = DateTime.Now;
            user.IPAddress = userViewModel.iPAddress;

            user.Name = userViewModel.name;
            user.Surname = userViewModel.surname;
            user.Username = userViewModel.username;
            user.Age = userViewModel.age;
            user.BirthDate = userViewModel.birthDate;
            user.Email = userViewModel.email;
            user.ProfessionId = userViewModel.professionId;

            return _userRepository.Update(user);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.Delete(id);
        }
    }
}
