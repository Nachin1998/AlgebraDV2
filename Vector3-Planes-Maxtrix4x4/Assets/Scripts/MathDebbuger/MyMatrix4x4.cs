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
            //X 0
            m00 = column0.x;
            m01 = column1.x;
            m02 = column2.x;
            m03 = column3.x;

            //1 Y
            m10 = column0.y;
            m11 = column1.y;
            m12 = column2.y;
            m13 = column3.y;

            //2 Z
            m20 = column0.z;
            m21 = column1.z;
            m22 = column2.z;
            m23 = column3.z;

            //3 W
            m30 = column0.w;
            m31 = column1.w;
            m32 = column2.w;
            m33 = column3.w;
        }
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: 
                        return m00;

                    case 1: 
                        return m10;

                    case 2: 
                        return m20;

                    case 3: 
                        return m30;

                    case 4: 
                        return m01;

                    case 5: 
                        return m11;

                    case 6: 
                        return m21;

                    case 7: 
                        return m31;

                    case 8: 
                        return m02;

                    case 9: 
                        return m12;

                    case 10:
                        return m22;

                    case 11:
                        return m32;

                    case 12:
                        return m03;

                    case 13:
                        return m13;

                    case 14:
                        return m23;

                    case 15:
                        return m33;

                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: 
                        m00 = value;
                        break;

                    case 1: 
                        m10 = value; 
                        break;

                    case 2: 
                        m20 = value; 
                        break;

                    case 3: 
                        m30 = value; 
                        break;

                    case 4: 
                        m01 = value; 
                        break;

                    case 5: 
                        m11 = value; 
                        break;

                    case 6: 
                        m21 = value; 
                        break;

                    case 7: 
                        m31 = value; 
                        break;

                    case 8: 
                        m02 = value; 
                        break;

                    case 9: 
                        m12 = value; 
                        break;

                    case 10: 
                        m22 = value; 
                        break;

                    case 11: 
                        m32 = value; 
                        break;

                    case 12: 
                        m03 = value; 
                        break;

                    case 13: 
                        m13 = value; 
                        break;

                    case 14: 
                        m23 = value; 
                        break;

                    case 15: 
                        m33 = value; 
                        break;
                }
            }
        }

        public float this[int row, int column]
        {
            get
            {
                return this[row + column * 4];
            }
            set
            {
                this[row + column * 4] = value;
            }
        }

        public static MyMatrix4x4 zero => new MyMatrix4x4(Vector4.zero, Vector4.zero, Vector4.zero, Vector4.zero);

        public static MyMatrix4x4 identity => new MyMatrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
        
        public static MyMatrix4x4 Rotate(Quaternion q)
        {
            MyMatrix4x4 mat = identity;

            mat.m02 = 2.0f * (q.x * q.z) + 2.0f * (q.y * q.w);
            mat.m12 = 2.0f * (q.y * q.z) - 2.0f * (q.x * q.w);
            mat.m22 = 1 - 2.0f * (q.x * q.x) - 2.0f * (q.y * q.y);

            mat.m00 = 1 - 2.0f * (q.y * q.y) - 2.0f * (q.z * q.z);
            mat.m10 = 2.0f * (q.x * q.y) + 2.0f * (q.z * q.w);
            mat.m20 = 2.0f * (q.x * q.z) - 2.0f * (q.y * q.w);

            mat.m01 = 2.0f * (q.x * q.y) - 2.0f * (q.z * q.w);
            mat.m11 = 1 - 2.0f * (q.x * q.x) - 2.0f * (q.z * q.z);
            mat.m21 = 2.0f * (q.y * q.z) + 2.0f * (q.x * q.w);

            return mat;
        }

        public static MyMatrix4x4 Scale(Vec3 vector)
        {
            MyMatrix4x4 m = identity;

            m.m00 *= vector.x;
            m.m11 *= vector.y;
            m.m22 *= vector.z;
            m.m33 = 1;

            return m;
        }

        public static MyMatrix4x4 Translate(Vec3 vector)
        {
            MyMatrix4x4 m = identity;

            m.m03 += vector.x;
            m.m13 += vector.y;
            m.m23 += vector.z;
            m.m33 = 1;
            return m;
        }

        public static MyMatrix4x4 TRS(Vec3 pos, Quaternion q, Vec3 s)
        {
            MyMatrix4x4 trs = zero;
            trs = Translate(pos) * Rotate(q) * Scale(s);
            return trs;
        }

        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0: 
                    return new Vector4(m00, m01, m02, m03);

                case 1: 
                    return new Vector4(m10, m11, m12, m13);

                case 2: 
                    return new Vector4(m20, m21, m22, m23);

                case 3: 
                    return new Vector4(m30, m31, m32, m33);

                default:
                    throw new IndexOutOfRangeException("Invalid row index!");
            }
        }

        public void SetColumn(int index, Vector4 column)
        {
            this[0, index] = column.x;
            this[1, index] = column.y;
            this[2, index] = column.z;
            this[3, index] = column.w;
        }

        public void SetTRS(Vec3 pos, Quaternion q, Vec3 s)
        {
            this = TRS(pos, q, s);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object other)
        {
            if (!(other is MyMatrix4x4)) return false;
            return Equals((MyMatrix4x4)other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(MyMatrix4x4 other)
        {
            return this == other;
        }

        public static Vector4 operator *(MyMatrix4x4 lhs, Vector4 vector)
        {
            Vector4 vec;
            vec.x = (lhs.m00 * vector.x) + (lhs.m01 * vector.y) + (lhs.m02 * vector.z) + (lhs.m03 * vector.w);
            vec.y = (lhs.m10 * vector.x) + (lhs.m11 * vector.y) + (lhs.m12 * vector.z) + (lhs.m13 * vector.w);
            vec.z = (lhs.m20 * vector.x) + (lhs.m21 * vector.y) + (lhs.m22 * vector.z) + (lhs.m23 * vector.w);
            vec.w = (lhs.m30 * vector.x) + (lhs.m31 * vector.y) + (lhs.m32 * vector.z) + (lhs.m33 * vector.w);
            return vec;
        }
        public static MyMatrix4x4 operator *(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
        {
            MyMatrix4x4 mat = new MyMatrix4x4(Vector4.zero, Vector4.zero, Vector4.zero, Vector4.zero);

            mat.m00 = (lhs.m00 * rhs.m00) + (lhs.m01 * rhs.m10) + (lhs.m02 * rhs.m20) + (lhs.m03 * rhs.m30);
            mat.m01 = (lhs.m00 * rhs.m01) + (lhs.m01 * rhs.m11) + (lhs.m02 * rhs.m21) + (lhs.m03 * rhs.m31);
            mat.m02 = (lhs.m00 * rhs.m02) + (lhs.m01 * rhs.m12) + (lhs.m02 * rhs.m22) + (lhs.m03 * rhs.m32);
            mat.m03 = (lhs.m00 * rhs.m03) + (lhs.m01 * rhs.m13) + (lhs.m02 * rhs.m23) + (lhs.m03 * rhs.m33);

            mat.m10 = (lhs.m10 * rhs.m00) + (lhs.m11 * rhs.m10) + (lhs.m12 * rhs.m20) + (lhs.m13 * rhs.m30);
            mat.m11 = (lhs.m10 * rhs.m01) + (lhs.m11 * rhs.m11) + (lhs.m12 * rhs.m21) + (lhs.m13 * rhs.m31);
            mat.m12 = (lhs.m10 * rhs.m02) + (lhs.m11 * rhs.m12) + (lhs.m12 * rhs.m22) + (lhs.m13 * rhs.m32);
            mat.m13 = (lhs.m10 * rhs.m03) + (lhs.m11 * rhs.m13) + (lhs.m12 * rhs.m23) + (lhs.m13 * rhs.m33);

            mat.m20 = (lhs.m20 * rhs.m00) + (lhs.m21 * rhs.m10) + (lhs.m22 * rhs.m20) + (lhs.m23 * rhs.m30);
            mat.m21 = (lhs.m20 * rhs.m01) + (lhs.m21 * rhs.m11) + (lhs.m22 * rhs.m21) + (lhs.m23 * rhs.m31);
            mat.m22 = (lhs.m20 * rhs.m02) + (lhs.m21 * rhs.m12) + (lhs.m22 * rhs.m22) + (lhs.m23 * rhs.m32);
            mat.m23 = (lhs.m20 * rhs.m03) + (lhs.m21 * rhs.m13) + (lhs.m22 * rhs.m23) + (lhs.m23 * rhs.m33);

            mat.m30 = (lhs.m30 * rhs.m00) + (lhs.m31 * rhs.m10) + (lhs.m32 * rhs.m20) + (lhs.m33 * rhs.m30);
            mat.m31 = (lhs.m30 * rhs.m01) + (lhs.m31 * rhs.m11) + (lhs.m32 * rhs.m21) + (lhs.m33 * rhs.m31);
            mat.m32 = (lhs.m30 * rhs.m02) + (lhs.m31 * rhs.m12) + (lhs.m32 * rhs.m22) + (lhs.m33 * rhs.m32);
            mat.m33 = (lhs.m30 * rhs.m03) + (lhs.m31 * rhs.m13) + (lhs.m32 * rhs.m23) + (lhs.m33 * rhs.m33);

            return mat;
        }
        public static bool operator ==(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
        {
            return (lhs.m00 == rhs.m00 && lhs.m01 == rhs.m01 && lhs.m02 == rhs.m02 && lhs.m03 == rhs.m03 &&
                    lhs.m10 == rhs.m10 && lhs.m11 == rhs.m11 && lhs.m12 == rhs.m12 && lhs.m13 == rhs.m13 &&
                    lhs.m20 == rhs.m20 && lhs.m21 == rhs.m21 && lhs.m22 == rhs.m22 && lhs.m23 == rhs.m23 &&
                    lhs.m30 == rhs.m30 && lhs.m31 == rhs.m31 && lhs.m32 == rhs.m32 && lhs.m33 == rhs.m33);
        }

        public static bool operator !=(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
        {
            return (lhs.m00 != rhs.m00 || lhs.m01 != rhs.m01 || lhs.m02 != rhs.m02 || lhs.m03 != rhs.m03 ||
                    lhs.m10 != rhs.m10 || lhs.m11 != rhs.m11 || lhs.m12 != rhs.m12 || lhs.m13 != rhs.m13 ||
                    lhs.m20 != rhs.m20 || lhs.m21 != rhs.m21 || lhs.m22 != rhs.m22 || lhs.m23 != rhs.m23 ||
                    lhs.m30 != rhs.m30 || lhs.m31 != rhs.m31 || lhs.m32 != rhs.m32 || lhs.m33 != rhs.m33);
        }
    }
}