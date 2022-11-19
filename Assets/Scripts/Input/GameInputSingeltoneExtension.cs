namespace Input
{
    public static class GameInputSingeltoneExtension
    {
        public static void EnableMenuInput(this GameInput ginp)
        {
            ginp.DisableAllInputs();
            ginp.UI.Enable();
        }

        public static void EnableGameplayInput(this GameInput ginp)
        {
            ginp.DisableAllInputs();
            ginp.Gameplay.Enable();
        }

        public static void DisableAllInputs(this GameInput ginp)
        {
            foreach (var inputAction in ginp)
            {
                inputAction.Disable();
            }
        }
    }
}