using System;
namespace CptS487_HW4
{
    public class Cone : Accessory
    {
        public override int AccessoryHealth
        {
            get; set;
        }

        public Cone()
        {
            this.AccessoryHealth = 25;
        }
    }
}
