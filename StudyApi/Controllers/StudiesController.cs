using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Controllers.Resources;

namespace StudyApi.Controllers
{
    [Route("api/[controller]")]
    public class StudiesController : Controller
    {
        // GET api/studies
        [HttpGet]
        public IEnumerable<StudyResource> Get()
        {
            return new StudyResource[] {
                StudyResource.Generate()
            };
        }
    }
}