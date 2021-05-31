﻿using System;
using UnityEngine;

namespace CustomMath
{
    public struct MyQuaternion : IEquatable<MyQuaternion>
    {
        public const float kEpsilon = 1E-06F;

        public float x;
        public float y;
        public float z;
        public float w;

        public static MyQuaternion identity => new MyQuaternion(0f, 0f, 0f, 1f);
        //public Vec3 eulerAngles { get; set; }
        public MyQuaternion normalized => Normalize(this);

        public MyQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        //
        // Resumen:
        //     Returns the angle in degrees between two rotations a and b.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        public static float Angle(MyQuaternion a, MyQuaternion b) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Creates a rotation which rotates angle degrees around axis.
        //
        // Parámetros:
        //   angle:
        //
        //   axis:
        public static MyQuaternion AngleAxis(float angle, Vec3 axis) { throw new NotImplementedException(); }
        public static MyQuaternion AxisAngle(Vec3 axis, float angle) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     The dot product between two rotations.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        public static float Dot(MyQuaternion a, MyQuaternion b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }
        //
        // Resumen:
        //     Returns a rotation that rotates z degrees around the z axis, x degrees around
        //     the x axis, and y degrees around the y axis.
        //
        // Parámetros:
        //   euler:
        public static MyQuaternion Euler(Vec3 euler) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Returns a rotation that rotates z degrees around the z axis, x degrees around
        //     the x axis, and y degrees around the y axis; applied in that order.
        //
        // Parámetros:
        //   x:
        //
        //   y:
        //
        //   z:
        public static MyQuaternion Euler(float x, float y, float z) { throw new NotImplementedException(); }
        public static MyQuaternion EulerAngles(float x, float y, float z) { throw new NotImplementedException(); }
        public static MyQuaternion EulerAngles(Vec3 euler) { throw new NotImplementedException(); }
        public static MyQuaternion EulerRotation(float x, float y, float z) { throw new NotImplementedException(); }
        public static MyQuaternion EulerRotation(Vec3 euler) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Creates a rotation which rotates from fromDirection to toDirection.
        //
        // Parámetros:
        //   fromDirection:
        //
        //   toDirection:
        public static MyQuaternion FromToRotation(Vec3 fromDirection, Vec3 toDirection) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Returns the Inverse of rotation.
        //
        // Parámetros:
        //   rotation:
        public static MyQuaternion Inverse(MyQuaternion rotation) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Interpolates between a and b by t and normalizes the result afterwards. The parameter
        //     t is clamped to the range [0, 1].
        //
        // Parámetros:
        //   a:
        //
        //   b:
        //
        //   t:
        public static MyQuaternion Lerp(MyQuaternion a, MyQuaternion b, float t) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Interpolates between a and b by t and normalizes the result afterwards. The parameter
        //     t is not clamped.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        //
        //   t:
        public static MyQuaternion LerpUnclamped(MyQuaternion a, MyQuaternion b, float t) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        //
        // Parámetros:
        //   forward:
        //     The direction to look in.
        //
        //   upwards:
        //     The vector that defines in which direction up is.

        public static MyQuaternion LookRotation(Vec3 forward) { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        //
        // Parámetros:
        //   forward:
        //     The direction to look in.
        //
        //   upwards:
        //     The vector that defines in which direction up is.

        public static MyQuaternion LookRotation(Vec3 forward, Vec3 upwards) //vec3 up
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Converts this MyQuaternion to one with the same orientation but with a magnitude
        //     of 1.
        //
        // Parámetros:
        //   q:
        public static MyQuaternion Normalize(MyQuaternion q)
        {
            float mag = Mathf.Sqrt(Dot(q, q));

            return mag < Mathf.Epsilon ? identity : new MyQuaternion(q.x / mag, q.y / mag, q.z / mag, q.w / mag);
        }
        //
        // Resumen:
        //     Rotates a rotation from towards to.
        //
        // Parámetros:
        //   from:
        //
        //   to:
        //
        //   maxDegreesDelta:
        public static MyQuaternion RotateTowards(MyQuaternion from, MyQuaternion to, float maxDegreesDelta)
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Spherically interpolates between MyQuaternions a and b by ratio t. The parameter
        //     t is clamped to the range [0, 1].
        //
        // Parámetros:
        //   a:
        //     Start value, returned when t = 0.
        //
        //   b:
        //     End value, returned when t = 1.
        //
        //   t:
        //     Interpolation ratio.
        //
        // Devuelve:
        //     A MyQuaternion spherically interpolated between MyQuaternions a and b.

        public static MyQuaternion Slerp(MyQuaternion a, MyQuaternion b, float t)
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Spherically interpolates between a and b by t. The parameter t is not clamped.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        //
        //   t:

        public static MyQuaternion SlerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
        { throw new NotImplementedException(); }
        public static Vec3 ToEulerAngles(MyQuaternion rotation)
        { throw new NotImplementedException(); }
        public bool Equals(MyQuaternion other)
        { throw new NotImplementedException(); }
        public override bool Equals(object other)
        { throw new NotImplementedException(); }

        public bool Equals(Quaternion other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        { throw new NotImplementedException(); }
        public void Normalize()
        {
            this = Normalize(this);
        }
        //
        // Resumen:
        //     Set x, y, z and w components of an existing MyQuaternion.
        //
        // Parámetros:
        //   newX:
        //
        //   newY:
        //
        //   newZ:
        //
        //   newW:
        public void Set(float newX, float newY, float newZ, float newW)
        { throw new NotImplementedException(); }

        public void SetAxisAngle(Vec3 axis, float angle)
        { throw new NotImplementedException(); }

        public void SetEulerAngles(Vec3 euler)
        { throw new NotImplementedException(); }

        public void SetEulerAngles(float x, float y, float z)
        { throw new NotImplementedException(); }

        public void SetEulerRotation(float x, float y, float z)
        { throw new NotImplementedException(); }

        public void SetEulerRotation(Vec3 euler)
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Creates a rotation which rotates from fromDirection to toDirection.
        //
        // Parámetros:
        //   fromDirection:
        //
        //   toDirection:
        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        //
        // Parámetros:
        //   view:
        //     The direction to look in.
        //
        //   up:
        //     The vector that defines in which direction up is.
        public void SetLookRotation(Vec3 view, Vec3 up) //el up es un vec3.up
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        //
        // Parámetros:
        //   view:
        //     The direction to look in.
        //
        //   up:
        //     The vector that defines in which direction up is.

        public void SetLookRotation(Vec3 view)
        { throw new NotImplementedException(); }
        public void ToAngleAxis(out float angle, out Vec3 axis)
        { throw new NotImplementedException(); }

        public void ToAxisAngle(out Vec3 axis, out float angle)
        { throw new NotImplementedException(); }

        public Vec3 ToEuler()
        { throw new NotImplementedException(); }

        public Vec3 ToEulerAngles()
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Returns a nicely formatted string of the MyQuaternion.
        //
        // Parámetros:
        //   format:
        public string ToString(string format)
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Returns a nicely formatted string of the MyQuaternion.
        //
        // Parámetros:
        //   format:
        public override string ToString()
        { throw new NotImplementedException(); }

        public static Vec3 operator *(MyQuaternion rotation, Vec3 point)
        {
            float x = rotation.x * 2F;
            float y = rotation.y * 2F;
            float z = rotation.z * 2F;

            float xx = rotation.x * x;
            float yy = rotation.y * y;
            float zz = rotation.z * z;

            float xy = rotation.x * y;
            float xz = rotation.x * z;
            float yz = rotation.y * z;

            float wx = rotation.w * x;
            float wy = rotation.w * y;
            float wz = rotation.w * z;

            Vec3 res;
            res.x = (1F - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
            res.y = (xy + wz) * point.x + (1F - (xx + zz)) * point.y + (yz - wx) * point.z;
            res.z = (xz - wy) * point.x + (yz + wx) * point.y + (1F - (xx + yy)) * point.z;
            return res;
        }
        public static MyQuaternion operator *(MyQuaternion lhs, MyQuaternion rhs)
        {
            return new MyQuaternion(
                  lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
                  lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
                  lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
                  lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
        }
        public static bool operator ==(MyQuaternion lhs, MyQuaternion rhs)
        {
            return lhs == rhs;
        }
        public static bool operator !=(MyQuaternion lhs, MyQuaternion rhs)
        {
            return lhs != rhs;
        }
    }
}
