using System.ComponentModel.DataAnnotations;

namespace DomainManager.Models
{
    public class Domain
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "域名不能为空")]
        [Display(Name = "域名")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "状态")]
        public DomainStatus Status { get; set; }

        [Display(Name = "注册日期")]
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "到期日期")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "备注")]
        [StringLength(200, ErrorMessage = "备注不能超过200字")]
        public string? Notes { get; set; }
    }

    public enum DomainStatus
    {
        [Display(Name = "可用")]
        Available = 1,

        [Display(Name = "已过期")]
        Expired = 2,

        [Display(Name = "即将到期")]
        Expiring = 3,

        [Display(Name = "已停用")]
        Suspended = 4
    }
}