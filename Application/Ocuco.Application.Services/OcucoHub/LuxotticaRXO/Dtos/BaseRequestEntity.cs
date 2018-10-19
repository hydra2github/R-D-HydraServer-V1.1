using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Application.Services.OcucoHub.LuxotticaRXO.Dtos
{
    public class BaseRequestEntity
    {
        public BaseRequestEntity()
        {

        }

        public virtual string Address { get; set; }

        public virtual int HttpRequest { get; set; }

        public virtual bool BasicAuth { get; set; }

        public virtual string BasicAuthUsername { get; set; }

        public virtual string BasicAuthPassword { get; set; }

    }
}
