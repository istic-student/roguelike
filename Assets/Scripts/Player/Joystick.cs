namespace Assets.Scripts.Player
{
    public class Joystick
    {

        public readonly string Horizontal;
        public readonly string Vertical;
        public readonly string Action;
        public readonly string Use;
        public readonly string Attack;

        public Joystick(int playerNumber)
        {
            Horizontal = "Horizontal_P" + playerNumber;
            Vertical = "Vertical_P" + playerNumber;
            Action = "Action_P" + playerNumber;
            Use = "Use_P" + playerNumber;
            Attack = "Attack_P" + playerNumber;
        }

    }
}
