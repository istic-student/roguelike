namespace Assets.Scripts.Interactive.Catchable
{
    public class Consumable : Abstract.Catchable
    {

        public ConsumableEnum Type;
        public int Value;

        public bool TypeAndValueEquals(Consumable consumable)
        {
            if (consumable == null)
                return false;

            return Type == consumable.Type && Value == consumable.Value;
        }

    }
}
