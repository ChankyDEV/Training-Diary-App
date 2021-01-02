using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BodyWeight
{
    public interface IAuth
    {
        Task<string> RegisterWithEmailAndPassword(String email, String password);
        Task LoginWithEmailAndPassword(String email, String password);
    }
}
