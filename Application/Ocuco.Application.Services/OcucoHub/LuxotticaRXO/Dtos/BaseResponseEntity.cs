using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Application.Services.OcucoHub.LuxotticaRXO.Dtos
{
    public class BaseResponseEntity
    {
        public virtual string WebService { get; set; }

        public virtual int ErrorCode { get; set; }

        public virtual string ErrorDescription { get; set; }
    }
}
