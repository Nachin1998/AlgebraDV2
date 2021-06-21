using System;
using UnityEngine;

namespace CustomMath
{
    public struct MyPlane : IEquatable<MyPlane>
    {
        #region Variables
        Vec3 normal;
        float distance;

        public Vec3 Normal { get { return normal; } set { normal = value; } }
        public float Distance { get { return distance; } set { distance = value; } }
        public MyPlane Flipped { get { return new MyPlane(-normal, -normal * distance); } }        
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

        public MyPlane(Vec3 inNormal, float d)
        {
            normal = inNormal.normalized;
            distance = d;
        }

        public MyPlane(Plane plane)
        {
            normal = new Vec3(plane.normal);
            distance = plane.distance;
        }
        #endregion


        #region Functions
        public static MyPlane Translate(MyPlane plane, Vec3 translation)
        {
            return new MyPlane(plane.normal, plane.distance += Vec3.Dot(plane.normal, translation));
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            float distanceAux = Vector3.Dot(normal, point) + distance;
            return point - (normal * distanceAux);
        }

        public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(normal, point) + distance;
        }

        public bool GetSide(Vec3 point)
        {
            if (GetDistanceToPoint(point) > 0f)
            {
                return true;
            }
            return false;
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            float dist1 = GetDistanceToPoint(inPt0);
            float dist2 = GetDistanceToPoint(inPt1);

            return (dist1 > 0.0f && dist2 > 0.0f) || (dist1 <= 0.0f && dist2 <= 0.0f);
        }

        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 side1 = b - a;
            Vec3 side2 = c - a;

            normal = Vec3.Cross(side1, side2).normalized;
            distance = -Vec3.Dot(normal, a);
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = -Vec3.Dot(inNormal, inPoint);
        }

        public void Translate(Vec3 translation)
        {
            distance += Vec3.Dot(normal, translation);
        }

        public bool Equals(Plane other)
        {
            return distance == other.distance && (Vec3)normal == (Vec3)other.normal;
        }

        public bool Equals(MyPlane other)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
