using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomMath
{
    public struct MyPlane : IEquatable<Plane>
    {
        #region Variables
        public Vec3 normal { get; set; }
        public float distance { get; set; }
        public MyPlane flipped { get { return new MyPlane(-normal, -normal * distance); } }
        #endregion


        #region Constructors
        public MyPlane(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = -Vec3.Dot(normal, inPoint);
        }

        public MyPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 side1 = b - a;
            Vec3 side2 = c - a;

            normal = Vec3.Cross(side1, side2).normalized;

            distance = -Vec3.Dot(normal, a);
        }
        #endregion


        #region Functions
        public static MyPlane Translate(MyPlane plane, Vec3 translation)
        {
            throw new NotImplementedException();
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public void Flip()
        {
            throw new NotImplementedException();
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public bool GetSide(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            throw new NotImplementedException();
        }

        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            throw new NotImplementedException();
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        public string ToString(string format)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vec3 translation)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Plane other)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
