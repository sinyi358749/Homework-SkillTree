using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_SkillTree.Models
{
    public partial class BookKeepingViewModel
    {

        public class NotFutureDateAttribute : ValidationAttribute
        {
            public override string FormatErrorMessage(string name)
            {
                return "日期不可大於今日";
            }

            public override bool IsValid(object value)
            {
                DateTime date = (DateTime)value;
                return date.Date <= DateTime.Today;
            }
        }

        public int? Id { get; set; }

        //類別
        [Display(Name = "類別")]
        [Required(ErrorMessage = "請選擇類別")]
        public string? Category { get; set; }

        //交易日期
        [Display(Name = "交易日期")]
        [Required(ErrorMessage = "請輸入交易日期")]
        [Column(TypeName = "datetime")]
        [NotFutureDate(ErrorMessage = "日期不可大於今日")]
        public DateTime? TransDate { get; set; }

        //交易金額
        [Display(Name = "交易金額")]
        [Required(ErrorMessage = "請輸入交易金額")]
        [Range(1, 2147483647, ErrorMessage = "金額不正確")]
        public int Amount { get; set; }

        //備註
        [Display(Name = "備註")]
        [StringLength(100, ErrorMessage = "備註內容不能超過 100 個字元")]
        public string? Description { get; set; }

        //guid流水號
        public Guid keyId { get; set; }

    }
}
