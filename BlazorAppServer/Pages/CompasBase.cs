using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppServer.Pages
{
    public class CompasBase:ComponentBase
    {
        public string Name { get; set; } = "Tom";
        public string Gender { get; set; } = "Male";
        public string Description { get; set; } = string.Empty;
    }
}
