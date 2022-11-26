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

        public static void DisablePlayerInput(this GameInput ginp)
        {
            ginp.Gameplay.Ability.Disable();
            ginp.Gameplay.Attack.Disable();
            ginp.Gameplay.Movement.Disable();
            ginp.Gameplay.Jump.Disable();
            ginp.Gameplay.Dash.Disable();
            ginp.Gameplay.Direction.Disable();
            ginp.Gameplay.ChangeWeapon.Disable();
            ginp.Gameplay.Grab.Disable();
        }

        public static void EnablePlayerInput(this GameInput ginp)
        {
            EnableGameplayInput(ginp);
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