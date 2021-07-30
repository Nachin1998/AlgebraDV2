using System;
using System.Collections;
using System.Collections.Generic;
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
            return EulerAngles(euler.x, euler.y, euler.z);
        }

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
            float angle = Angle(from, to);
            return angle == 0f ? to : Lerp(from, to, Mathf.Min(1.0f, maxDegreesDelta / angle));
        }

        public static MyQuaternion Slerp(MyQuaternion a, MyQuaternion b, float t)
        {
            t = Mathf.Clamp(t, 0, 1);
            return SlerpUnclamped(a, b, t); 
        }

        public static MyQuaternion SlerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
        {
            float num1;
            float num2;

            MyQuaternion q;
            float dot = Dot(a, b);
            bool neg = false;

            if (dot < 0f)
            {
                neg = true;
                dot = -dot;
            }

            if (dot >= 1.0f)
            {
                num1 = 1.0f - t;
                num2 = neg ? -t : t;
            }
            else
            {
                float num3 = (float)Math.Acos(dot);
                float num4 = (float)(1.0 / Math.Sin(num3));

                num1 = ((float)Math.Sin(((1f - t) * num3))) * num4;
                num2 = neg ? (((float)-Math.Sin((t * num3))) * num4) : (((float)Math.Sin((t * num3))) * num4);
            }

            q.x = ((num1 * a.x) + (num2 * b.x));
            q.y = ((num1 * a.y) + (num2 * b.y));
            q.z = ((num1 * a.z) + (num2 * b.z));
            q.w = ((num1 * a.w) + (num2 * b.w));
            return q;
        }


        public bool Equals(MyQuaternion other)
        {
            return this == other;
        }

        public override bool Equals(object other)
        {
            if (!(other is MyQuaternion)) return false;
            return Equals((MyQuaternion)other);
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

        public void SetEulerAngles(Vector3 euler)
        {
            SetEulerAngles(euler.x, euler.y, euler.z);
        }

        public void SetEulerAngles(float x, float y, float z)
        {
            Vector3 eulerAngles = Vector3.zero;
            eulerAngles.x = Mathf.Atan2(2 * x * w - 2 *y * z, 1 - 2 * (x * x) - 2 * (z * z)) * Mathf.Rad2Deg;
            eulerAngles.y = Mathf.Atan2(2 * y * w - 2 *x * z, 1 - 2 * (y * y) - 2 * (z * z)) * Mathf.Rad2Deg;
            eulerAngles.z = Mathf.Asin(2 * x * y + 2 * z * w) * Mathf.Rad2Deg;
            this.eulerAngles = eulerAngles;
        }

        public Vector3 ToEuler()
        {
            Vector3 a = Vector3.zero;
            a.x = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * (x * x) - 2 * (z * z)) * Mathf.Rad2Deg;
            a.y = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * (y * y) - 2 * (z * z)) * Mathf.Rad2Deg;
            a.z = Mathf.Asin(2 * x * y + 2 * z * w) * Mathf.Rad2Deg;
            return a;
        }

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
