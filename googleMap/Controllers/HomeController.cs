using googleMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Web.Mvc;
using System.Text;

namespace googleMap.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(Details model)
        {

            ViewBag.LatLng = getLatLng();
            if (Request.IsAjaxRequest() == true)
            {



                Details m = new Details();
                m.zipcode = model.zipcode;

                ViewBag.LatLng = getLatLng(m);



                return PartialView("polygon");

            }

            
            return View();


          

        }
        public LatLng getLatLng()
        {
            LatLng c = new LatLng();
            c.centre = "{ lat:32.30868,lng: -88.421453}";
            c.coordinate = "{ lat:33.30868,lng: -88.421453}";
            return c;

        }
        public LatLng getLatLng(Details models)
        {
            LatLng c1 = new LatLng();

            XElement xdoc1 = XElement.Load("Give file path");
            //retrieving latitude and longitude from kml file
            var k = (from b in xdoc1.Descendants("Placemark")
                     where ((from a in b.Elements("ExtendedData").Elements("SchemaData").Elements("SimpleData")
                             where a.Attribute("name").Value == "ZCTA5CE10"
                             select a.Value).ToArray()).Any(t => (t) == models.zipcode) == true
                     from a1 in b.Elements("Polygon").Elements("outerBoundaryIs").Elements("LinearRing").Elements("coordinates")
                     select (string)a1.Value).ToArray().FirstOrDefault();

           

            string[] d = k.Split(',');
          



            StringBuilder s = new StringBuilder();
            s.Append("[");

            for (int i = 0; i < d.Length - 1; i = i + 2)
            {
                s.Append("{ lat:");
                s.Append(d[i + 1]);
                s.Append(",lng:");
                s.Append(d[i] + "}");
                if (i < d.Length - 3)
                {
                    s.Append(",");
                }
            }

            c1.coordinate = s.Append("]").ToString();
           
            c1.centre = "{ lat:32.308242,lng: 0 -88.386912}";
            return c1;
                }
        }
    }
