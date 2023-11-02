using CCA.API.Model;
using CCA.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CCA.API.Controllers
{
    [Route( "v1/startement/" )]
    public class StartementController : Controller
    {
        [HttpGet]
        [Route( "get-all" )]
        [AllowAnonymous]
        public async Task<ActionResult<List<CurrentAccountStatementModel>>> GetAll() => CCARepository.GetAll().Result;

        [HttpGet]
        [Route( "get-byid/{id:int}" )]
        [AllowAnonymous]
        public async Task<CurrentAccountStatementModel> GetByID( int id ) => await CCARepository.GetById( id );

        [HttpPost]
        [Route( "create" )]
        public async Task<ActionResult<CurrentAccountStatementModel>> Create( [FromBody] CurrentAccountStatementModel model )
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
        public async Task<ActionResult<CurrentAccountStatementModel>> UpdateADD( [FromBody] CurrentAccountStatementModel model )
        {
            if (!ModelState.IsValid)
                return BadRequest( ModelState );

            if (model.Id <= 0)
                return NotFound( new { message = "dado não encontrado" } );

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
        public async Task<CurrentAccountStatementModel> Delete( int id ) => await CCARepository.Delete( id );

        [HttpPut]
        [Route( "cancel/{id:int}" )]
        [AllowAnonymous]
        public async Task<CurrentAccountStatementModel> Cancel( int id ) => await CCARepository.Cancel( id );



    }
}
