using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomMath
{
    //
    // Resumen:
    //     Representation of a plane in 3D space.
    public struct MyPlane
    {

        public MyPlane(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = -Vec3.Dot(normal, inPoint);
        }

        public MyPlane(Vec3 inNormal, float d)
        {
            //
            // Resumen:
            //     Creates a plane.
            //
            // Parámetros:
            //   inNormal:
            //
            //   d:
            throw new NotImplementedException();
        }

        public MyPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 side1 = b - a;
            Vec3 side2 = c - a;

            normal = Vec3.Cross(side1, side2).normalized;

            distance = -Vec3.Dot(normal, a);
        }

        //
        // Resumen:
        //     Normal vector of the plane.
        public Vec3 normal { get; set; }
        //
        // Resumen:
        //     The distance measured from the Plane to the origin, along the Plane's normal.
        public float distance { get; set; }
        //
        // Resumen:
        //     Returns a copy of the plane that faces in the opposite direction.
        public MyPlane flipped { get { return new MyPlane(-normal, -normal * distance); } }


        public static MyPlane Translate(MyPlane plane, Vec3 translation)
        {
            //
            // Resumen:
            //     Returns a copy of the given plane that is moved in space by the given translation.
            //
            // Parámetros:
            //   plane:
            //     The plane to move in space.
            //
            //   translation:
            //     The offset in space to move the plane with.
            //
            // Devuelve:
            //     The translated plane.
            throw new NotImplementedException();
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            //
            // Resumen:
            //     For a given point returns the closest point on the plane.
            //
            // Parámetros:
            //   point:
            //     The point to project onto the plane.
            //
            // Devuelve:
            //     A point on the plane that is closest to point.
            throw new NotImplementedException();
        }

        public void Flip()
        {
            //
            // Resumen:
            //     Makes the plane face in the opposite direction.
            throw new NotImplementedException();
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            //
            // Resumen:
            //     Returns a signed distance from plane to point.
            //
            // Parámetros:
            //   point:
            throw new NotImplementedException();
        }

        public bool GetSide(Vec3 point)
        {
            //
            // Resumen:
            //     Is a point on the positive side of the plane?
            //
            // Parámetros:
            //   point:
            throw new NotImplementedException();
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            //
            // Resumen:
            //     Are two points on the same side of the plane?
            //
            // Parámetros:
            //   inPt0:
            //
            //   inPt1:
            throw new NotImplementedException();
        }

        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            //
            // Resumen:
            //     Sets a plane using three points that lie within it. The points go around clockwise
            //     as you look down on the top surface of the plane.
            //
            // Parámetros:
            //   a:
            //     First point in clockwise order.
            //
            //   b:
            //     Second point in clockwise order.
            //
            //   c:
            //     Third point in clockwise order.
            throw new NotImplementedException();
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            //
            // Resumen:
            //     Sets a plane using a point that lies within it along with a normal to orient
            //     it.
            //
            // Parámetros:
            //   inNormal:
            //     The plane's normal vector.
            //
            //   inPoint:
            //     A point that lies on the plane.
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
            //
            // Resumen:
            //     Moves the plane in space by the translation vector.
            //
            // Parámetros:
            //   translation:
            //     The offset in space to move the plane with.
            throw new NotImplementedException();
        }
    }
}
