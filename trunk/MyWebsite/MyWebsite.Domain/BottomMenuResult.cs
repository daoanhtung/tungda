using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Domain
{
    public class BottomMenuResult
    {
        public int BottomMenuId { get; set; }
        public string MenuText { get; set; }
        public string MenuLink { get; set; }
        public bool Status { get; set; }
    }
}
