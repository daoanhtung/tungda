using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Domain
{
    public class SelectMenuResult
    {
        public int MenuId { get; set; }
        public string MenuText { get; set; }
        public string MenuText_VI { get; set; }
        public string MenuLink { get; set; }
        public int ParentMenuId { get; set; }
    }
}
