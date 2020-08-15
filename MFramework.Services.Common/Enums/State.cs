using System.ComponentModel.DataAnnotations;

namespace MFramework.Services.Common.Enums
{
    public enum State
    {
        [Display(Name = "Aktif")]
        Active = 0,
        [Display(Name = "Pasif")]
        Passive = 1,
        [Display(Name = "Silindi")]
        Deleted = 3,
        [Display(Name = "Arşivlendi")]
        Archived = 4
    }
}
