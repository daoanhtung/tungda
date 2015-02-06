using MyWebsite.DataAccess.IRepositories;
using MyWebsite.DataAccess.Models;
using MyWebsite.DataAccess.Repositories;
using MyWebsite.Domain;
using MyWebsite.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MyWebsite.Business.Implement
{
    public class SliderManager : ISliderManager
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderManager(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public SliderManager()
        {
            _sliderRepository = new SliderRepository();
        }

        #region LocalDB
        public DataReturn<SliderResult> Create(SliderResult entity)
        {
            return ModelMapping.MapDataReturn<Slider, SliderResult>(_sliderRepository.Create(ModelMapping.Map<Slider>(Encryption.Encrypt<SliderResult>(entity))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataReturn<SliderResult> Edit(SliderResult entity)
        {
            return ModelMapping.MapDataReturn<Slider, SliderResult>(_sliderRepository.Edit(ModelMapping.Map<Slider>(Encryption.Encrypt<SliderResult>(entity))));
        }

        public DataReturn<SliderResult> Delete(SliderResult entity)
        {
            return ModelMapping.MapDataReturn<Slider, SliderResult>(_sliderRepository.Delete(ModelMapping.Map<Slider>(entity)));
        }

        public DataReturn<SliderResult> Delete(int id)
        {
            return ModelMapping.MapDataReturn<Slider, SliderResult>(_sliderRepository.Delete(x => x.SliderId.Equals(id)));
        }

        public SliderResult Details(int id)
        {
            return Encryption.Decrypt<SliderResult>(ModelMapping.Map<SliderResult>(_sliderRepository.Single(e => e.SliderId.Equals(id))));
        }

        public IEnumerable<SliderResult> SelectAll()
        {
            return _sliderRepository.All().Select(ModelMapping.Map<SliderResult>).Select(Encryption.Decrypt<SliderResult>);
        }

        public IEnumerable<SliderResult> SelectActive()
        {
            return _sliderRepository.All().Where(x=> x.Status==true).Select(ModelMapping.Map<SliderResult>).Select(Encryption.Decrypt<SliderResult>);
        }

        public IEnumerable<SliderResult> SelectDeactive()
        {
            return _sliderRepository.All().Where(x=> x.Status==false).Select(ModelMapping.Map<SliderResult>).Select(Encryption.Decrypt<SliderResult>);
        }
        #endregion
    }
}
