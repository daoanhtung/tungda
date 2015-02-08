using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebsite.Business;
using MyWebsite.Business.Implement;
using MyWebsite.Domain;
using MyWebsite.Models;
using MyWebsite.Utils;

namespace MyWebsite.Controllers
{
    public class SliderController : Controller
    {
        //

        private readonly ISliderManager _sliderManager;
        public SliderController(ISliderManager sliderManager)
        {
            _sliderManager = sliderManager;
        }
        public SliderController()
        {
            _sliderManager = new SliderManager();
        }

        // GET: /Slider/
        private SliderViewModel Entity
        {
            get
            {
                if (Session[Constants.SliderSessionKey] == null)
                {
                    Session[Constants.SliderSessionKey] = new SliderViewModel
                    {
                        Sliders = _sliderManager.SelectActive().ToList()
                    };
                }
                else
                {
                    Session[Constants.SliderSessionKey] = new SliderViewModel
                    {
                        Sliders = _sliderManager.SelectActive().ToList()
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
