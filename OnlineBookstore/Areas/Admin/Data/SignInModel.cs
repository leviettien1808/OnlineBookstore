using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Ánh xạ các trường theo trang Sign In admin.
/// </summary>

namespace OnlineBookstore.Areas.Admin.Data
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Mời nhập Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mời nhập Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}