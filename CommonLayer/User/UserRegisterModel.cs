using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace CommonLayer.User
{
    public class UserRegisterModel
    {
        [Required]
        //[RegularExpression(@"^[A-Z]{1}[A-Za-z]{3,}", ErrorMessage = "Start with a capital letter and must have minimum three letters")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public long Phone_Num { get; set; }
    }
}
