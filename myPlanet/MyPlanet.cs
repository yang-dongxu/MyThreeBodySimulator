using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MySimulatorOfThreeBody
{
    public class MyPlanet
    {
        protected Vector3 Position;
        protected Vector3 Speed;
        protected float Mass;
        protected float Size;

        public MyPlanet(float x = 0, float y =0, float z=0, float vx=0, float vy=0, float vz=0, float mass=0,float size=0.5f)
        {
            Position = new Vector3(x, y, z);
            Speed = new Vector3(vx, vy, vz);
            Mass = mass;
            Size = size;

        }

        public MyPlanet(Vector3 x, Vector3 v,float mass=0,float size=0.5f)
        {
            Position = x;
            Speed = v;
            Mass = mass;
            Size = size;
        }

        public Vector3 GetPosition()
        {
            return (Position);
        }

        public Vector3 GetSpeed()
        {
            return (Speed);
        }

        public float GetMass()
        {
            return Mass;
        }

        private Vector3 ChangePosition(float t)
        {
            Position = Position + Speed * t;
            return (Position);
        }

        private Vector3 ChangeSpeed(Vector3 a, float t)
        {
            Speed = Speed + a * t;
            return Speed;
        }

        public Vector3 Step(Vector3 a, float t = 1)
        {
            ChangeSpeed(a, t);
            ChangePosition(t);
            return Position;
        }

        public Vector3 GetA(MyPlanet another,float step_time=1f,float G=1f)
        {
            Vector3 distance = Position - another.GetPosition();
            var direction = -Vector3.Normalize(distance);
            float distanceLength = Vector3.DistanceSquared(Position, another.GetPosition());
            Vector3 a;
            if (distanceLength>Size+another.Size)
            { 
                a = (G * another.GetMass() * direction) / distanceLength; 
            }
            else
            {
                //这是完全弹性碰撞公式
                var m1 = Mass;
                var m2 = another.Mass;
                var v1 = Speed;
                var v2 = another.Speed;
                var new_v1 = ((m1 - m2) * v1 + 2 * m2 * v2) / ((m1 + m2));
                a = (new_v1-v1)/step_time;
            }

            return a;
        }


        public static Vector3[] GetAs(MyPlanet p1, MyPlanet p2,float step_time,float G=1f)
        {
            Vector3 a1 = p1.GetA(p2,step_time,G);
            Vector3 a2 = p2.GetA(p1, step_time, G);
            Vector3[] all = new Vector3[2] { a1, a2 };
            return all;
        }
    }
}
