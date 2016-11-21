using System;

namespace Business_Data
{

    public interface IAuthenticable
    {
        
        bool Connect(string UserID, string UserPassword);
    }
    
}