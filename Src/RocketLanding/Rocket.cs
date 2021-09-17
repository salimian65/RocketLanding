using System;

namespace RocketLanding
{
    public class Rocket
    {
        private static readonly object Obj = new();

        public Rocket(string name, Land landingSpace)
        {
            Name = name;
            LandingSpace = landingSpace;
        }

        public string Name { get; private set; }

        public Land LandingSpace { get; private set; }



        public LandingStatus Landing(int x, int y)
        {
            lock (Obj)
            {
                var checkForLanding = LandingSpace.CheckForLanding(x, y);
                if (checkForLanding == PointStatus.OkForLanding)
                {
                    LandingSpace.SelectThePoint(x, y);
                    return LandingStatus.LandigHasBeenDone;
                }
                else
                {
                    Enum.TryParse(checkForLanding.ToString(), out LandingStatus landingStatus);
                    return landingStatus;
                }
            }
        }
    }
}