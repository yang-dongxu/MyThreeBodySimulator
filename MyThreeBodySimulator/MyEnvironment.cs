using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;



namespace MyThreeBodySimulator
{
    public class MyEnvironment
    {
        protected Dictionary<int, MyPlanet> Environment = new Dictionary<int, MyPlanet>();
        private int __NextID = 0;

        public MyEnvironment(string config="InitializeConfig.csv")
        {
            StreamReader sr = new StreamReader(config);
            string line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                var lineSplit = line.Split(',');
                float[] nums = new float[8];
                for(int i = 0; i < 8; i++)
                {
                    nums[i] = float.Parse(lineSplit[i]);
                }
                Vector3 position = new Vector3(nums[0], nums[1], nums[2]);
                Vector3 speed = new Vector3(nums[3], nums[4], nums[5]);
                float mass = nums[6];
                float size = nums[7];
                AddPlanet(position, speed, mass, size);
            }
            sr.Close();
        }

        public bool AddPlanet(float x = 0, float y = 0, float z = 0, float vx = 0, float vy = 0, float vz = 0, float mass = 0, float size = 0.5f)
        {
            var position = new Vector3(x, y, z);
            var speed = new Vector3(vx, vy, vz);
            if (__NextID > 0)
            {
                foreach (MyPlanet old in Environment.Values)
                {
                    if (Vector3.Equals(old.GetPosition(), position))
                    {
                        return false;
                    }
                }
            }
            MyPlanet planet = new MyPlanet(position, speed, mass, size);
            Environment.Add(__NextID, planet);
            __NextID += 1;
            return true;
        }

        public bool AddPlanet(MyPlanet p)
        {
            Vector3 position = p.GetPosition();
            if (__NextID > 0)
            {
                foreach (MyPlanet old in Environment.Values)
                {
                    if (Vector3.Equals(old.GetPosition(), position))
                    {
                        return false;
                    }
                }
            }
            Environment.Add(__NextID, p);
            __NextID += 1;
            return true;
        }

        public bool AddPlanet(Vector3 x, Vector3 v, float mass = 0, float size = 0.5f)
        {
            var position = x;
            var speed = v;
            if (__NextID > 0)
            {
                foreach (MyPlanet old in Environment.Values)
                {
                    if (Vector3.Equals(old.GetPosition(), position))
                    {
                        return false;
                    }
                }
            }
            MyPlanet planet = new MyPlanet(position, speed, mass, size);
            Environment.Add(__NextID, planet);
            __NextID += 1;
            return true;
        }


        public Dictionary<int, Vector3>[] StepMove(float step_time = 0.1f, float G = 1f)
        {
            Dictionary<int, Vector3> id_to_accelerations = new Dictionary<int, Vector3>();
            foreach (int akey in Environment.Keys)
            {
                MyPlanet aplanet = Environment[akey];
                for (int bkey = akey + 1; bkey < __NextID; bkey++)
                {
                    MyPlanet bplanet = Environment[bkey];
                    Vector3[] accelerations = MyPlanet.GetAs(aplanet, bplanet, step_time, G);
                    if (id_to_accelerations.ContainsKey(akey))
                    {
                        id_to_accelerations[akey] += accelerations[0];
                    }
                    else
                    {
                        id_to_accelerations.Add(akey, accelerations[0]);
                    }
                    if (id_to_accelerations.ContainsKey(bkey))
                    {
                        id_to_accelerations[akey] += accelerations[1];
                    }
                    else
                    {
                        id_to_accelerations.Add(bkey, accelerations[1]);
                    }
                }

            }
            Dictionary<int, Vector3> id_to_position = new Dictionary<int, Vector3>();
            Dictionary<int, Vector3> id_to_speed = new Dictionary<int, Vector3>();
            foreach (int akey in Environment.Keys)
            {
                MyPlanet aplanet = Environment[akey];
                Vector3 p = aplanet.Step(id_to_accelerations[akey], step_time);
                id_to_position.Add(akey, p);
                id_to_speed.Add(akey, aplanet.GetSpeed());
            }

            return new Dictionary<int, Vector3>[] { id_to_position, id_to_speed, id_to_accelerations };
        }

        public List<Dictionary<int, Vector3>> StepsMove(int steps =1, int steps_pic= 1,float step_time = 0.1f, float G = 1f)
        {
            List<Dictionary<int, Vector3>> positions = new List<Dictionary<int, Vector3>>();
            for(int i = 1; i <= steps; i++)
            {
                var z = StepMove(step_time, G: G);
                if (i % steps_pic == 0)
                {
                    positions.Add(z[0]);
                }
            }
            return positions;
        }
    }
}


