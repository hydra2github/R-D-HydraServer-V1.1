using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Application.Services.OcucoHub.LuxotticaRXO.Dtos
{
    public class CheckFrameFullRequest : BaseRequestEntity
    {
        [Required]
        public string Kunnr { get; set; }
        [Required]
        public string Upc { get; set; }
    }
}
