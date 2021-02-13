using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EnrichMyCareService.ViewModel
{
    public class ImageVideoViewModel
    {
        public List<IFormFile> files { get; set; }
    }
}