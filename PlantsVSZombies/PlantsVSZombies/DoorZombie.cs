using System;
namespace CptS487_HW4
{
    public class DoorZombie : Zombie
    {
        public override int Health
        {
            get; set;
        }

        public override char Type
        {
            get; set;
        }

        public DoorZombie()
        {
            this.Health = 75;
            this.Type = 'D';
        }

        public override void TakeDamage(int damage)
        {
            this.Health -= damage;
        }
    }
}
