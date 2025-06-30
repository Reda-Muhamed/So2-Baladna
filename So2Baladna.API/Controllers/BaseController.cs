using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using So2Baladna.Core.Interfaces;

namespace So2Baladna.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController: ControllerBase
    {
        protected readonly IUnitOfWork unitWork;
        private readonly IMapper mapper;

        public BaseController(IUnitOfWork unitWork , IMapper mapper)
        {
            this.unitWork = unitWork;
            this.mapper = mapper;
        }
    }
}
