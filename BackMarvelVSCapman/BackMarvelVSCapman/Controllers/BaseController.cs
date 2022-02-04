using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace BackMarvelVSCapman.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;

        protected BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
