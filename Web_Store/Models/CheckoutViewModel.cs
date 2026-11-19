using System.ComponentModel.DataAnnotations;

namespace Web_Store.Models
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thành phố")]
        public string City { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập quốc gia")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; }

        public bool AcceptTerms { get; set; }
        public decimal TotalAmount { get; set; } = 140;
    }
}
