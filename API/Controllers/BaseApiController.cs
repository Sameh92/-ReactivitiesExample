using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        /* just in case we already have this _mediator because we are inside our base API controller,a shared controller
         amongst many controllers and if we have mediator available for whatever reason then we are going to attemp to reuse that 
         if not then we are going to go and get the mediotor service and use that instead 

        */

        // asa we are inside the context of an an API or inside the contct of a conroller 
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        // public int MyProperty { get => 1; set { MyProperty = value; } }
    }
}