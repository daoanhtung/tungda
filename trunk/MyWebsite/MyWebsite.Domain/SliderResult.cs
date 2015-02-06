using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Domain
{
    public class SliderResult
    {
        public int SliderId { get; set; }
        public string Html { get; set; }
        public string TransitionStyle { get; set; }
        public int TransitionSpeed { get; set; }
        public int SlotAmount { get; set; }
        public string Link { get; set; }
        public bool Status { get; set; }
    }
}
