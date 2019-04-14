// ReSharper disable InconsistentNaming
namespace SGDE.Domain.ViewModels
{
    #region Using

    using System;

    #endregion

    public class UserViewModel : BaseEntityViewModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public int age { get; set; }
        public DateTime? birthDate { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public int professionId { get; set; }
        public string professionName { get; set; }
    }
}
