using System.ComponentModel.DataAnnotations;

namespace Homework_SkillTree.Models
{
    enum Category
    {
        支出 = 0,
        收入 = 1,
    }

    public partial class AccountBookModel
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "請輸入交易類別")]
        public int Categoryyy { get; set; }

        [Required(ErrorMessage = "請輸入交易金額")]
        public int Amounttt { get; set; }

        [Required(ErrorMessage = "請輸入交易日期")]
        public DateTime Dateee { get; set; }

        [Required(ErrorMessage = "請輸入備註")]
        public string Remarkkk { get; set; } 

        public int TotalCount { get; set; } // 用於存儲總筆數
    }
}
