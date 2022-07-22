using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Micro_api.Models;

namespace Micro_Helper.MicroServicies.Auth
{
    public interface IusuarioServicio
    {

        Task<microHelper_data> Autenticacion(AuthUserPeti user);
    }
}
