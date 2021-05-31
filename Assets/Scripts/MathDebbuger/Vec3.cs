using System;
using UnityEngine;

namespace CustomMath
{
    [System.Serializable]
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude { get { return (x * x + y * y + z * z); } }
        public Vec3 normalized { get { return new Vec3(x / magnitude, y / magnitude, z / magnitude); } }
        public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            bool equalsX = left.x == right.x;
            bool equalsY = left.y == right.y;
            bool equalsZ = left.z == right.z;
            return (equalsX && equalsY && equalsZ);
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(scalar * v3.x, scalar * v3.y, scalar * v3.z);
        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector2(v2.x, v2.y);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        public static float Angle(Vec3 from, Vec3 to)
        {
            return Mathf.Acos(Dot(from, to) / (from.magnitude * to.magnitude)) * Mathf.Rad2Deg; 
            //Arcoseno de producto punto entre ambos vectores sobre la magnitud de ambos vectores multiplicadas FUA
        }
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            if (vector.magnitude > maxLength) return vector.normalized * maxLength;
            else return vector;
        }
        public static float Magnitude(Vec3 vector)
        {
            return vector.magnitude;
        }
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3((a.y * b.z) - (b.y * a.z), 
                            (a.z * b.x) - (b.z * a.x), 
                            (a.x * b.y) - (b.x * a.y));
        }
        public static float Distance(Vec3 a, Vec3 b)
        {
            return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + 
                              (a.y - b.y) * (a.y - b.y) + 
                              (a.z - b.z) * (a.z - b.z));
        }
        public static float Dot(Vec3 a, Vec3 b)
        {
            return (a.x * b.x) + 
                   (a.y * b.y) + 
                   (a.z * b.z);
        }
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            float aux = Mathf.Clamp(t, 0, 1);
            return a + (b - a) * aux;
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            return a + (b - a) * t;
        }
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            Vec3 auxVec;
            if (a.x > b.x) auxVec.x = a.x;
            else auxVec.x = b.x;
            if (a.y > b.y) auxVec.y = a.y;
            else auxVec.y = b.y;
            if (a.z > b.z) auxVec.z = a.z;
            else auxVec.z = b.z;
            return auxVec;
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            Vec3 auxVec;
            if (a.x < b.x) auxVec.x = a.x;
            else auxVec.x = b.x;
            if (a.y < b.y) auxVec.y = a.y;
            else auxVec.y = b.y;
            if (a.z < b.z) auxVec.z = a.z;
            else auxVec.z = b.z;
            return auxVec;
        }
        public static float SqrMagnitude(Vec3 vector)
        {
            return vector.sqrMagnitude;
        }
        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            return (Dot(vector, onNormal) / (onNormal.magnitude * onNormal.magnitude)) * onNormal;
        }
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            return inDirection - (2 * Dot(inDirection, inNormal) * inNormal); //hay operadores en la matematica que no son conmutativos
        }
        public void Set(float newX, float newY, float newZ)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
        }
        public void Scale(Vec3 scale)
        {
            this.x = x * scale.x;
            this.y = y * scale.y;
            this.z = z * scale.z;
        }
        public void Normalize()
        {
            this.x = x / magnitude;
            this.y = y / magnitude;
            this.z = z / magnitude;
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}