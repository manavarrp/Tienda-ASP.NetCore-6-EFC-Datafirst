using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALESSYSTEM.BLL.Interfaces
{
    public interface IUtilitiesService
    {
        string CreatePassword();

        string ConvertSha256(string text);
    }
}
