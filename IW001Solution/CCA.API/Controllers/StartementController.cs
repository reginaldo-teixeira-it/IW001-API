using CCA.API.Model;
using CCA.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CCA.API.Controllers
{
    [Route( "v1/startement" )]
    public class StartementController : Controller
    {
        [HttpGet]
        [Route( "get-all" )]
        [AllowAnonymous]
        public async Task<ActionResult<List<CurrentAccountStatement>>> GetAll() => CCARepository.GetAll().Result;

        [HttpGet]
        [Route( "get-byid/{id:int}" )]
        [AllowAnonymous]
        public async Task<CurrentAccountStatement> GetByID( int id ) => await CCARepository.GetById( id );

        [HttpPost]
        [Route( "create" )]
        public async Task<ActionResult<CurrentAccountStatement>> Create( [FromBody] CurrentAccountStatement model )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await CCARepository.Create( model );

                return model;
            }
            catch (Exception)
            {
                return BadRequest( new { message = "Não foi possível criar o cadastro" } );

            }
        }

        [HttpPut]
        [Route( "update" )]
        public async Task<ActionResult<CurrentAccountStatement>> Update( [FromBody] CurrentAccountStatement model )
        {
            if (!ModelState.IsValid)
                return BadRequest( ModelState );

            if (model.Id <= 0)
                return NotFound( new { message = "dado não encontrad" } );

            try
            {
                await CCARepository.Update( model );

                return model;
            }
            catch (Exception)
            {
                return BadRequest( new { message = "Não foi possível atualizar o cadastro" } );

            }
        }

        [HttpDelete]
        [Route( "delete/{id:int}" )]
        [AllowAnonymous]
        public async Task<CurrentAccountStatement> Delete( int id ) => await CCARepository.Delete( id );

        [HttpPut]
        [Route( "cancel/{id:int}" )]
        [AllowAnonymous]
        public async Task<CurrentAccountStatement> Cancel( int id ) => await CCARepository.Cancel( id );



    }
}
