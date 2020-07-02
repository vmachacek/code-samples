using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessingPipeline.Domain.ValueObjects
{
    public class GeoCoordinate
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }

        public bool IsInPoligon(List<GeoCoordinate> polygon)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Lat < this.Lat && polygon[j].Lat >= this.Lat || polygon[j].Lat < this.Lat && polygon[i].Lat >= this.Lat)
                {
                    if (polygon[i].Lng + (this.Lat - polygon[i].Lat) / (polygon[j].Lat - polygon[i].Lat) * (polygon[j].Lng - polygon[i].Lng) < this.Lng)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        public GeoCoordinate(decimal lat, decimal lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public GeoCoordinate()
        {


        }

        public double DistanceTo(GeoCoordinate targetCoordinates)
        {
            return DistanceTo(targetCoordinates, UnitOfLength.Kilometers);
        }

        public double DistanceTo(GeoCoordinate targetCoordinates, UnitOfLength unitOfLength)
        {
            var baseRad = Math.PI * Convert.ToDouble(Lat) / 180;
            var targetRad = Math.PI * Convert.ToDouble(targetCoordinates.Lat) / 180;
            var theta = Convert.ToDouble(Lng - targetCoordinates.Lng);
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return unitOfLength.ConvertFromMiles(dist);
        }
    }


    public class UnitOfLength
    {
        public static UnitOfLength Kilometers = new UnitOfLength(1.609344);
        public static UnitOfLength NauticalMiles = new UnitOfLength(0.8684);
        public static UnitOfLength Miles = new UnitOfLength(1);

        private readonly double _fromMilesFactor;

        private UnitOfLength(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }

        public double ConvertFromMiles(double input)
        {
            return input * _fromMilesFactor;
        }
    }
}
