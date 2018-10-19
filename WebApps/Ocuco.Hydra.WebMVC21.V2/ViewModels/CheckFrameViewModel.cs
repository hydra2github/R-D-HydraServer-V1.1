using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.V2.ViewModels
{
    public class CheckFrameViewModel
    {
        [Required]
        [MinLength(5)]
        public string Kunnr { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Too Long")]
        public string FrameUpc { get; set; }
    }
}
