using System;

namespace PandaGo
{
    public class Customer : User
    {
        public override void ShowInfor() 
        {
        Console.WriteLine($"Khách hàng tên {Name} có số điện thoại {PhoneNumber}");
        }
    }

}
