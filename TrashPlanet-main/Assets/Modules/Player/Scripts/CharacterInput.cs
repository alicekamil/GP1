namespace GP1.Gameplay
{
    public struct CharacterInput
    {
        public float Move;

        public static CharacterInput Empty => new() { Move = 0 };
    }
}