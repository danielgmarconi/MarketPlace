using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string value);
        bool Valid(string encryptedValue,string comparedValue);
    }
}
