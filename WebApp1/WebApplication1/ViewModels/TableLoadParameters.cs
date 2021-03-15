using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class TableLoadParameters
    {
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string TableId { get; set; }
    }
}
