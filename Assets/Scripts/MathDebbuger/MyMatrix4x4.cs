using System;
using UnityEngine;

namespace CustomMath
{
    public struct MyMatrix4x4 : IEquatable<MyMatrix4x4>
    {
        public float m00;
        public float m33;
        public float m23;
        public float m13;
        public float m03;
        public float m32;
        public float m22;
        public float m02;
        public float m12;
        public float m21;
        public float m11;
        public float m01;
        public float m30;
        public float m20;
        public float m10;
        public float m31;

        public MyMatrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            throw new NotImplementedException();
        }
        public static MyMatrix4x4 zero
        {
            get
            {
                return new MyMatrix4x4(new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0));
            }
        }

        public static MyMatrix4x4 identity
        {
            get
            {
                return new MyMatrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            }
        }

        public static MyMatrix4x4 Rotate(MyQuaternion q)
        {
            throw new NotImplementedException();
        }

        public static MyMatrix4x4 Scale(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public static MyMatrix4x4 Translate(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public static MyMatrix4x4 TRS(Vec3 pos, MyQuaternion q, Vec3 s)
        {
            throw new NotImplementedException();
        }

        public Vector4 GetRow(int index)
        {
            throw new NotImplementedException();
        }

        public void SetColumn(int index, Vector4 column)
        {
            throw new NotImplementedException();
        }

        public void SetTRS(Vec3 pos, Quaternion q, Vec3 s)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object other)
        {
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(MyMatrix4x4 other)
        {
            throw new NotImplementedException();
        }

        public static Vector4 operator *(MyMatrix4x4 lhs, Vector4 vector)
        {
            throw new NotImplementedException();
        }
        public static MyMatrix4x4 operator *(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
        {
            throw new NotImplementedException();
        }
        public static bool operator ==(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
        {
            throw new NotImplementedException();
        }
    }
}