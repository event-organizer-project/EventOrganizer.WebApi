using AutoFixture;
namespace EventOrganizer.Test
{
    public class CustomFixture : Fixture
    {
        public CustomFixture()
        {
            Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => Behaviors.Remove(b));
            Behaviors.Add(new OmitOnRecursionBehavior());

            Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        }
    }
}
