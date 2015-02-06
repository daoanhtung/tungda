using System;
using System.Collections.Generic;
using MyWebsite.Domain;
using MyWebsite.Utils;

namespace MyWebsite.Business
{
    public interface ISliderManager
    {
        DataReturn<SliderResult> Create(SliderResult entity);
        DataReturn<SliderResult> Edit(SliderResult entity);
        DataReturn<SliderResult> Delete(SliderResult entity);
        DataReturn<SliderResult> Delete(int id);
        SliderResult Details(int id);
        IEnumerable<SliderResult> SelectAll();
        IEnumerable<SliderResult> SelectActive();
        IEnumerable<SliderResult> SelectDeactive();
    }
}
