using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RocketLanding
{
    public class Land
    {
        public Land(string name, int landingAreaSide, int landingPlatformSide, Point allowedStartPoint)
        {
            Name = name;
            LandingAreaSide = landingAreaSide;
            LandingPlatformSide = landingPlatformSide;
            AllowedStatePoint = allowedStartPoint;
            Points = new ConcurrentDictionary<string, Point>();

            for (var x = 1; x <= landingAreaSide; x++)
            {
                for (var y = 1; y <= landingAreaSide; y++)
                {
                    var key = Utility.GetKey(x, y);
                    var allowedXEnd = allowedStartPoint.X + landingPlatformSide;
                    var allowedYEnd = allowedStartPoint.Y + landingPlatformSide;

                    if (x >= allowedStartPoint.X && x < allowedXEnd &&
                        y >= allowedStartPoint.Y && y < allowedYEnd)
                    {
                        Points.TryAdd(key, new Point(x, y, PointStatus.OkForLanding));
                    }
                    else
                    {
                        Points.TryAdd(key, new Point(x, y, PointStatus.OutOfPlatform));
                    }
                }
            }
        }

        public string Name { get; private set; }

        public int LandingAreaSide { get; private set; }

        public int LandingPlatformSide { get; private set; }

        public Point AllowedStatePoint { get; private set; }

        public ConcurrentDictionary<string, Point> Points { get; private set; }

        public PointStatus GetLandStatus(int x, int y)
        {
            var key = Utility.GetKey(x, y);

            if (Points.ContainsKey(key))
            {
                return Points[key].PointStatus;
            }

            throw new Exception("point out of landing area");
        }

        public PointStatus CheckForLanding(int x, int y)
        {
            var ddd = GetLandStatus(x, y);

            if (ddd != PointStatus.OkForLanding)
            {
                return ddd;
            }

            var sss = FindVicinityPoints( x, y);

            if (sss.ClashPoints.Any())
            {
                return PointStatus.Clash;
            }


            return PointStatus.OkForLanding;
        }

        public void SelectThePoint(int x, int y)
        {
            for (var xx = x - 1; xx <= x + 1; xx++)
            {
                for (var yy = y - 1; yy <= y + 1; yy++)
                {
                    var key = Utility.GetKey(xx, yy);

                    if (Points[key].PointStatus != PointStatus.OutOfPlatform)
                    {
                        Points[key].SetStatus(PointStatus.Clash); 
                    }
                }
            }
           
            throw new Exception("point out of landing area");
        }

        public List<Point> GetOkForLandingPoints()
        {
            return Points.Values.Where(a => a.PointStatus == PointStatus.OkForLanding).ToList();
        }


        private LandingResultDto FindVicinityPoints(int x, int y)
        {
            var landingResultDto = new LandingResultDto();

            for (var xx = x - 1; xx <= x + 1; xx++)
            {
                for (var yy = y - 1; yy <= y + 1; yy++)
                {
                    var key = Utility.GetKey(xx, yy);

                    if (Points[key].PointStatus == PointStatus.Clash)
                    {
                        landingResultDto.ClashPoints.Add(Points[key]);
                    }

                    if (Points[key].PointStatus == PointStatus.OkForLanding)
                    {
                        landingResultDto.OkForLandingPoints.Add(Points[key]);
                    }

                    if (Points[key].PointStatus == PointStatus.OutOfPlatform)
                    {
                        landingResultDto.OutOfPlatformPoints.Add(Points[key]);
                    }
                }
            }

            return landingResultDto;
        }
    }

   // public class 
}