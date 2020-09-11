using Microsoft.AspNetCore.Identity;

namespace Album.Models
{
    public class AppUser : IdentityUser
    {
        // Khai báo thêm các thuộc tính ngoài các thuộc
        // tính như UserName, Email ... cung cấp sẵn bởi IdentityUser
    }
}