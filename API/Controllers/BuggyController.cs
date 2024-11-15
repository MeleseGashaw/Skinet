using Microsoft.AspNetCore.Mvc;
using  API.Errors;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class BuggyController: BaseApiController
    {
        private readonly StoreContext _context;
        public  BuggyController(StoreContext context)
        {
            _context=context;
        }

       
        [HttpGet("testAuth")]
        [Authorize]

        public ActionResult<string> GetSecretText()
        {
            return "secret Stuff";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing =_context.Products.Find(42);
            if(thing==null)
            {
                return NotFound(new ApiResponse (404)); 
            }
            return Ok();
        }

        
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        { 
            var thing =_context.Products.Find(42);
            var thingtoreturne=thing.ToString();
            return Ok();
        }

        
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
 
        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

    }
}