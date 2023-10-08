using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMovies.Utilites
{
    public class UserServiceFake : IUserService
    {
        public string GetUserId()
        {
            return "Vijay 2";
        }
    }
}