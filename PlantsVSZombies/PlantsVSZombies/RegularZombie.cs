using System;
namespace CptS487_HW4
{
    public class RegularZombie : Zombie
    {
        public override int Health
        {
            get; set;
        }

        public override char Type
        {
            get; set;
        }

        public RegularZombie()
        {
            this.Health = 50;
            this.Type = 'R';
        }

        public RegularZombie(int health)
        {
            this.Health = health;
            this.Type = 'R';
        }

        public override void TakeDamage(int damage)
        {
            this.Health -= damage;
        }
    }
}
