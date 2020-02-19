using System;
namespace CptS487_HW4
{
    public class Door : Accessory
    {
        public override int AccessoryHealth
        {
            get; set;
        }

        public Door()
        {
            this.AccessoryHealth = 25;
        }
    }
}
