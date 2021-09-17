using FluentAssertions;
using RocketLanding;
using Xunit;

namespace TestRocketLanding
{
    public class LandingTests
    {
        [Fact]
        public void Returns_oKForLanding_when_asks_for_position_5_5()
        {   // Fixture setup
            var land = LandFixtureSetup();
            
            var rocket = new Rocket("Falcon 9", landingSpace: land);
            var result = rocket.LandingSpace.GetLandStatus(5, 5);
            
            //Assertion
            result.Should().Be(PointStatus.OkForLanding);
        }

        [Fact]
        public void Returns_outOfPlatform_when_asks_for_position_16_15()
        {   // Fixture setup
            var land = LandFixtureSetup();

            var rocket = new Rocket("Falcon 9", land);
            var result = rocket.LandingSpace.GetLandStatus(16, 15);

            //Assertion
            result.Should().Be(PointStatus.OutOfPlatform);
        }

        [Fact]
        public void Returns_clash_when_another_rocket_want_to_landing_in_a_same_position()
        {   // Fixture setup
            var land = LandFixtureSetup();

            var rocket = new Rocket("Falcon 9", land);
           rocket.Landing(7,7);

            //Assertion
            result.Should().Be(PointStatus.OutOfPlatform);
        }




        private Land LandFixtureSetup()
        {
            var name = "SpaceX Landing Zone 1";
            var landingAreaSide = 100;
            var landingPlatformSide = 10;
            var allowedStartPoint = new Point(5, 5, PointStatus.OkForLanding);

            return new Land(name,
                                landingAreaSide,
                                landingPlatformSide,
                                allowedStartPoint);
        }
    }
}