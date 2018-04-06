using googleMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace googleMap.ViewModel
{
    public class GoogleViewModel
    {
        public Details Zipcode { get; set; }
        public LatLng  latlng { get; set; }
    }
}