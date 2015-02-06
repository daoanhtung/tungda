using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebsite.Domain;
using MyWebsite.Models;
using MyWebsite.Utils;

namespace MyWebsite.Controllers
{
    public class SliderController : Controller
    {
        //
        // GET: /Slider/
        private SliderViewModel Entity
        {
            get
            {
                if (Session[Constants.SliderSessionKey] == null)
                {
                    Session[Constants.SliderSessionKey] = new SliderViewModel
                    {
                        Sliders = new List<SliderResult>()
                    };
                }
                else
                {
                    Session[Constants.SliderSessionKey] = new SliderViewModel
                    {
                        Sliders = new List<SliderResult>()
                    };
                }

                return (SliderViewModel)Session[Constants.SliderSessionKey];
            }
        }
        public ActionResult Index()
        {
            return PartialView(Entity);
        }

    }
}
