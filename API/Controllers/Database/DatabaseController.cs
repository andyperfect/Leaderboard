using API.Controllers.Database.Models;
using API.Controllers.Filters;
using Microsoft.AspNetCore.Mvc;
using Services.DatabaseInitialization;
using Services.Windsor;

namespace API.Controllers.Database
{
    [ApiController]
    [StandardExceptionFilter]
    [Route("Database")]
    public class DatabaseController : Controller
    {
        private readonly IDatabaseInitializationService _databaseInitializationService;
        public DatabaseController()
        {
             _databaseInitializationService = IoC.Container.Resolve<IDatabaseInitializationService>();
        }
        
        [HttpPost, Route("initialize")]
        public ActionResult Initialize([FromBody] InitializeModel model)
        {
            _databaseInitializationService.InitializeDatabase(model.Email, model.Username, model.Password);
            return new OkResult();
        }
    }
}
