using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Domain
{
    public class AdminMenuResult
    {
        public int AdminMenuId { get; set; }
        public string AdminMenuLink { get; set; }
        public string AdminMenuText { get; set; }
        public int ParentAdminMenuId { get; set; }
        public int MaxPerColumn { get; set; }
    }
}
