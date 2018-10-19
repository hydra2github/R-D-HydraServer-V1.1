using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocuco.Application.Services.OcucoHub.LuxotticaRXO;
using Ocuco.Application.Services.OcucoHub.LuxotticaRXO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.V2.Controllers.API
{
    //
    // Core 2.0
    //

    [Route("api/[Controller]")]
    public class CheckFrameController : Controller
    {
        private readonly ILogger<CheckFrameController> logger;
        private readonly ILuxotticaRXOWSService rxoService;

        public CheckFrameController(ILogger<CheckFrameController> logger,
                                    ILuxotticaRXOWSService rxoService)
        {
            this.logger = logger;
            this.rxoService = rxoService;
        }

        [HttpGet("{id}", Name = "GetContacts")]
        public async Task<IActionResult> GetById(string id)
        {
            //var item = await ContactsRepo.Find(id);
            //if (item == null)
            //{
            //    return NotFound();
            //}
            //return Ok(item);
            return Ok();
        }


        [HttpPost]
        public IActionResult Post([FromBody]CheckFrameRequest model)
        {
            var response = rxoService.CallCheckFrame(model);

            return Ok(response);
        }


        [HttpPost]
        [Route("V2")]
        public async Task<IActionResult> CheckFrame([FromBody] CheckFrameRequest model)
        {
            //    //cancellationToken.ThrowIfCancellationRequested();
            //    //var response = await _editPatientServices.CreatePatient(CreateServiceRequest(newDetails));
            //    //return await response.BuildResponse(this, HttpStatusCode.Created);


            if (model == null)
            {
                return BadRequest();
            }
            //    await ContactsRepo.Add(item);
            var response = await rxoService.CallCheckFrameAsync(model);

            //return CreatedAtRoute("CheckFrame", new { Controller = "Contacts", id = item.MobilePhone }, item);

            return Ok();
        }

    }
}
