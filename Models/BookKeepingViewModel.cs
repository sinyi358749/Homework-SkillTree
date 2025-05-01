using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_SkillTree.Models
{
    public partial class BookKeepingViewModel
    {

        public int? Id { get; set; }

        //類別
        [Display(Name = "類別")]
        [Required(ErrorMessage = "請選擇類別")]
        public string? Category { get; set; }

        //交易日期
        [Display(Name = "交易日期")]
        [Required(ErrorMessage = "請輸入交易日期")]
        [Column(TypeName = "datetime")]
        public DateTime? TransDate { get; set; }

        //交易金額
        [Display(Name = "交易金額")]
        [Required(ErrorMessage = "請輸入交易金額")]
        public double? Amount { get; set; }

        //備註
        [Display(Name = "備註")]
        [StringLength(100, ErrorMessage = "備註內容不能超過 100 個字元")]
        public string? Description { get; set; }

        //guid流水號
        public Guid keyId { get; set; }

    }
}
