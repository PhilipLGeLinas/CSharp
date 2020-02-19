using System;
namespace CptS487_HW4
{
    public class ZombieFactory
    {
        public ZombieFactory()
        {
        }

        public Zombie CreateZombie(String type)
        {
            switch (type)
            {
                case "1":
                    return new RegularZombie();
                case "2":
                    return new ConeZombie();
                case "3":
                    return new BucketZombie();
                case "4":
                    return new DoorZombie();
                default:
                    return null;
            }
        }
    }
}
