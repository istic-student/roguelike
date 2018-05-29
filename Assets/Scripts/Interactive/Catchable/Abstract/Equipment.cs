namespace Assets.Scripts.Interactive.Catchable.Abstract
{
    public class Equipment : Interactive.Abstract.Catchable
    {

        public EquipmentEnum Type;
        public float DamageAbsorption;
        public float Attack;

        public virtual void Use()
        {
        }

    }
}
