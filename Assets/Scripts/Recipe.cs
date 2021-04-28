namespace Planets
{
    public class Recipe
    {
        public Slot Input { get; }
        public Slot Output { get; }
        public float CraftSpeed { get; }

        public Recipe(Slot input, Slot output, float craftSpeed)
        {
            Input = input;
            Output = output;
            CraftSpeed = craftSpeed;
        }
    }
}
