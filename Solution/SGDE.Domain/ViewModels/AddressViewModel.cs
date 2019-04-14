// ReSharper disable InconsistentNaming
namespace SGDE.Domain.ViewModels
{
    #region Using

    #endregion

    public class AddressViewModel : BaseEntityViewModel
    {
        public string street { get; set; }
        public int number { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
    }
}
