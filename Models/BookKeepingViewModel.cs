using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_SkillTree.Models
{
    public partial class BookKeepingViewModel : IValidatableObject
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
        public int Amount { get; set; }

        //備註
        [Display(Name = "備註")]
        public string? Description { get; set; }

        //guid流水號
        public Guid keyId { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TransDate > DateTime.Today)
            {
                yield return new ValidationResult("日期不可大於今日", new string[] { "TransDate" });
            }

            //如果金額小於等於0
            if (Amount <= 0)
            {
                yield return new ValidationResult("金額不可小於等於0", new string[] { "Amount" });
            }
            //金額必須是整數
            if (Amount % 1 != 0)
            {
                yield return new ValidationResult("金額必須是整數", new string[] { "Amount" });
            }


            //如果備註長度大於100
            if (Description != null && Description.Length > 100)
            {
                yield return new ValidationResult("備註內容不能超過 100 個字元", new string[] { "Description" });

            }
        }





    }
}