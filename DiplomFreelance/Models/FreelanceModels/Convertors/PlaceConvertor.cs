using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class PlaceConvertor
    {
        public static PlaceViewModel ConvertToPlaceViewModel(this DomainPlace place)
        {
            PlaceViewModel item = new PlaceViewModel()
            {
                ID = place.ID,
                Name = place.Name
            };
            return item;
        }
        public static List<PlaceViewModel> ConvertToPlaceViewModel(this List<DomainPlace> place)
        {
            List<PlaceViewModel> list = new List<PlaceViewModel>() { };
            foreach (var item in place)
            {
                list.Add(item.ConvertToPlaceViewModel());
            }
            return list;
        }
        public static DomainPlace ConvertToPlaceDomainModel(this Place place)
        {
            DomainPlace item = new DomainPlace()
            {
                ID = place.ID,
                Name = place.Name,
            };
            return item;
        }
        public static List<DomainPlace> ConvertToPlaceDomainModel(this List<Place> place)
        {
            List<DomainPlace> list = new List<DomainPlace>() { };
            foreach (var item in place)
            {
                list.Add(item.ConvertToPlaceDomainModel());
            }
            return list;
        }
    }
}