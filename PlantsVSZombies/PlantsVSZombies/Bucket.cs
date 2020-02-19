using System;
namespace CptS487_HW4
{
    public class Bucket : Accessory
    {
        public override int AccessoryHealth
        {
            get; set;
        }

        public Bucket()
        {
            this.AccessoryHealth = 75;
        }
    }
}
