using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Lưu những thông tin cần thiết khi đăng nhập thành công và
/// được sử dụng ở nhiều nơi.
/// </summary>

namespace OnlineBookstore
{
    [Serializable]
    public class UserLogin
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}