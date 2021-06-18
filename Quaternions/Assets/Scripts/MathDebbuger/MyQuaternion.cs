using System;
using UnityEngine;

namespace CustomMath
{
    [System.Serializable]
    public struct MyQuaternion : IEquatable<MyQuaternion>
    {
        public const float kEpsilon = 1E-06F;

        public float x;
        public float y;
        public float z;
        public float w;

        public static MyQuaternion identity => new MyQuaternion(0f, 0f, 0f, 1f);
        public Vector3 eulerAngles
        {
            get
            {
                Vector3 a = Vector3.zero;
                a.x = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * (x * x) - 2 * (z * z)) * Mathf.Rad2Deg;
                a.y = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * (y * y) - 2 * (z * z)) * Mathf.Rad2Deg;
                a.z = Mathf.Asin(2 * x * y + 2 * z * w) * Mathf.Rad2Deg;
                return a;
            }
            set
            {
                MyQuaternion q = Euler(value);
                x = q.x;
                y = q.y;
                z = q.z;
                w = q.w;
            }
        }

        public MyQuaternion normalized => Normalize(this);

        public MyQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public MyQuaternion(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        public MyQuaternion(MyQuaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }
        //
        // Resumen:
        //     Returns the angle in degrees between two rotations a and b.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        public static float Angle(MyQuaternion a, MyQuaternion b)
        {
            MyQuaternion inv = Inverse(a);
            MyQuaternion result = b * inv;

            return Mathf.Acos(Mathf.Min(Mathf.Abs(Dot(a, b)), 1.0F)) * 2.0F * Mathf.Rad2Deg;
        }
        //
        // Resumen:
        //     Creates a rotation which rotates angle degrees around axis.
        //
        // Parámetros:
        //   angle:
        //
        //   axis:
        public static MyQuaternion AngleAxis(float angle, Vector3 axis)
        {
            angle *= Mathf.Deg2Rad * 0.5f;
            axis.Normalize();

            MyQuaternion auxQuat;
            auxQuat.x = axis.x * Mathf.Sin(angle);
            auxQuat.y = axis.y * Mathf.Sin(angle);
            auxQuat.z = axis.z * Mathf.Sin(angle);
            auxQuat.w = Mathf.Cos(angle);

            return auxQuat.normalized;
        }

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
        public static MyQuaternion Euler(Vector3 euler)
        {
            MyQuaternion qX = identity;
            MyQuaternion qY = identity;
            MyQuaternion qZ = identity;

            float sin = Mathf.Sin(Mathf.Deg2Rad * euler.x * 0.5f);
            float cos = Mathf.Cos(Mathf.Deg2Rad * euler.x * 0.5f);
            qX.Set(sin, 0.0f, 0.0f, cos);

            sin = Mathf.Sin(Mathf.Deg2Rad * euler.y * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * euler.y * 0.5f);
            qY.Set(0.0f, sin, 0.0f, cos);

            sin = Mathf.Sin(Mathf.Deg2Rad * euler.z * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * euler.z * 0.5f);
            qZ.Set(0.0f, 0.0f, sin, cos);

            return new MyQuaternion(qX * qY * qZ);
        }
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
        public static MyQuaternion Euler(float x, float y, float z)
        {
            x *= Mathf.Deg2Rad;
            y *= Mathf.Deg2Rad;
            z *= Mathf.Deg2Rad;

            MyQuaternion q = new MyQuaternion();
            q.x = Mathf.Sin(x * 0.5f);
            q.y = Mathf.Sin(y * 0.5f);
            q.z = Mathf.Sin(z * 0.5f);
            q.w = Mathf.Cos(x * 0.5f) * Mathf.Cos(y * 0.5f) * Mathf.Cos(z * 0.5f) -
                  Mathf.Sin(x * 0.5f) * Mathf.Sin(y * 0.5f) * Mathf.Sin(z * 0.5f);
            q.Normalize();
            return q;
        }

        public static MyQuaternion EulerAngles(float x, float y, float z)
        {
            throw new NotImplementedException();
        }
        public static MyQuaternion EulerAngles(Vector3 euler)
        {
            throw new NotImplementedException();
        }
        public static MyQuaternion EulerRotation(float x, float y, float z)
        {
            throw new NotImplementedException();
        }
        public static MyQuaternion EulerRotation(Vector3 euler)
        {
            throw new NotImplementedException();
        }
        //
        // Resumen:
        //     Creates a rotation which rotates from fromDirection to toDirection.
        //
        // Parámetros:
        //   fromDirection:
        //
        //   toDirection:
        public static MyQuaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        {
            throw new NotImplementedException();
        }
        //
        // Resumen:
        //     Returns the Inverse of rotation.
        //
        // Parámetros:
        //   rotation:
        public static MyQuaternion Inverse(MyQuaternion rotation)
        {
            return new MyQuaternion(-rotation.x, -rotation.y, -rotation.z, rotation.w);
        }

        public static MyQuaternion Lerp(MyQuaternion a, MyQuaternion b, float t)
        {
            t = Mathf.Clamp(t, 0, 1);
            return LerpUnclamped(a, b, t);
        }

        public static MyQuaternion LerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
        {
            MyQuaternion difference = new MyQuaternion(b.x - a.x, b.y - a.y, b.z - a.z, b.w - b.w);
            MyQuaternion differenceLerped = new MyQuaternion(difference.x * t, difference.y * t, difference.z * t, difference.w * t);

            return new MyQuaternion(a.x + differenceLerped.x, a.y + differenceLerped.y, a.z + differenceLerped.z, a.w + differenceLerped.w).normalized;
        }
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

        public static MyQuaternion LookRotation(Vector3 forward)
        {
            throw new NotImplementedException();
        }
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

        public static MyQuaternion LookRotation(Vector3 forward, Vector3 upwards) //Vector3 up
        {
            throw new NotImplementedException();
        }
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
        {
            throw new NotImplementedException();
        }
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
        public static Vector3 ToEulerAngles(MyQuaternion rotation)
        { throw new NotImplementedException(); }
        public bool Equals(MyQuaternion other)
        {
            return this == other;
        }

        public override bool Equals(object other)
        {
            if (!(other is MyQuaternion)) return false;
            return Equals((MyQuaternion)other);
        }

        public bool Equals(Quaternion other)
        {
            throw new NotImplementedException();
        }

        public void Normalize()
        {
            this = Normalize(this);
        }

        public void Set(float newX, float newY, float newZ, float newW)
        {
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
        }

        public void SetAxisAngle(Vector3 axis, float angle)
        { throw new NotImplementedException(); }

        public void SetEulerAngles(Vector3 euler)
        { throw new NotImplementedException(); }

        public void SetEulerAngles(float x, float y, float z)
        { throw new NotImplementedException(); }

        public void SetEulerRotation(float x, float y, float z)
        { throw new NotImplementedException(); }

        public void SetEulerRotation(Vector3 euler)
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Creates a rotation which rotates from fromDirection to toDirection.
        //
        // Parámetros:
        //   fromDirection:
        //
        //   toDirection:
        public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
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
        public void SetLookRotation(Vector3 view, Vector3 up) //el up es un Vector3.up
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

        public void SetLookRotation(Vector3 view)
        { throw new NotImplementedException(); }
        public void ToAngleAxis(out float angle, out Vector3 axis)
        { throw new NotImplementedException(); }

        public void ToAxisAngle(out Vector3 axis, out float angle)
        { throw new NotImplementedException(); }

        public Vector3 ToEuler()
        { throw new NotImplementedException(); }

        public Vector3 ToEulerAngles()
        { throw new NotImplementedException(); }
        //
        // Resumen:
        //     Returns a nicely formatted string of the MyQuaternion.
        //
        // Parámetros:
        //   format:
        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ", " + w.ToString() + ")";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Vector3 operator *(MyQuaternion rotation, Vector3 point)
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

            Vector3 res;
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
        public static MyQuaternion operator *(MyQuaternion lhs, Quaternion rhs)
        {
            return new MyQuaternion((lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z + lhs.z * rhs.y), 
                                        (lhs.w * rhs.y - lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x), 
                                        (lhs.w * rhs.z + lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w), 
                                        (lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z));
        }
        public static MyQuaternion operator *(Quaternion lhs, MyQuaternion rhs)
        {
            return new MyQuaternion((lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z + lhs.z * rhs.y), 
                                        (lhs.w * rhs.y - lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x), 
                                        (lhs.w * rhs.z + lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w), 
                                        (lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z));
        }
        public static bool operator ==(MyQuaternion lhs, MyQuaternion rhs)
        {
            return lhs == rhs;
        }
        public static bool operator !=(MyQuaternion lhs, MyQuaternion rhs)
        {
            return lhs != rhs;
        }

        public static implicit operator Quaternion(MyQuaternion q)
        {
            return new Quaternion(q.x, q.y, q.z, q.w);
        }
        public static implicit operator MyQuaternion(Quaternion q)
        {
            return new MyQuaternion(q.x, q.y, q.z, q.w);
        }
    }
}
