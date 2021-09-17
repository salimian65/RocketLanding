using System.Collections.Generic;

namespace RocketLanding
{
    public class LandingResultDto
    {

        public LandingResultDto()
        {
            ClashPoints = new List<Point>();
            OutOfPlatformPoints = new List<Point>();
            OkForLandingPoints = new List<Point>();
        }

        public List<Point> ClashPoints { get;  set; }

        public List<Point> OutOfPlatformPoints { get; set; }

        public List<Point> OkForLandingPoints { get; set; }
    }
}