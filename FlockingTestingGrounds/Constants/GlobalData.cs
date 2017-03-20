using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingTestingGrounds.Constants
{
    static class GlobalData
    {
        //Higher value gives wilder formation
        public static float ALIGNMENTMODIFIER = 10;

        //Lower value gives stronger attraction to center
        public static float COHESIONMODIFIER = 10;

        //Lower value gives lower pull towards it
        public static float DESTINATIONMODIFIER = 5;

        //The distance keeping them apart
        public static float SEPARATIONDISTANCE = 15;

        //A value between 0-1 gives the weight of the separation, 1 being 100%
        public static float SEPARATIONMODIFIER = 1;

        //Boid Data
        public static float BOIDSPEED = 2f;

        //Flock Data
        public static int FLOCKSIZE = 200;

        public static int m1 = 1;

        public static int m2 = 1;

        public static int m3 = 1;

        public static int m4 = 1;

        public static int screenWidth, screenHeight;

        public static void Formation()
        {
            ALIGNMENTMODIFIER = 1;
            COHESIONMODIFIER = 10;
            SEPARATIONDISTANCE = 10;
            SEPARATIONMODIFIER = 1;
            DESTINATIONMODIFIER = 1;
        }

        public static void Swarm()
        {
            ALIGNMENTMODIFIER = 50;
            COHESIONMODIFIER = 20;
            SEPARATIONDISTANCE = 10;
            SEPARATIONMODIFIER = 5;
            DESTINATIONMODIFIER = 2;
        }

        public static void Chaotic()
        {
            ALIGNMENTMODIFIER = 1000;
            COHESIONMODIFIER = 1;
            SEPARATIONDISTANCE = 10;
            SEPARATIONMODIFIER = 1f;
            DESTINATIONMODIFIER = 3;

        }

        public static void AssembleFlock()
        {
            m1 = 1;
            m2 = 1;
            m3 = 1;
            m4 = 1;
        }

        public static void ScatterFlock()
        {
            m1 = -1;
            m2 = -1;
            m3 = -1;
            m4 = -1;
        }
    }
}
