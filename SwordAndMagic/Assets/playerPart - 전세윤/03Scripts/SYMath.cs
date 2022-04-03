using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������, ������ ��ƿ��Ƽ �Լ��� ��Ƴ��� �� �Դϴ�. https://www.youtube.com/watch?v=997G_JrJ7iA

namespace SYMath
{
    public static class SYMath
    {

        //����, ����, ���� ���� https://shakddoo.tistory.com/entry/c-%EA%B0%81%EB%8F%84-%EB%9D%BC%EB%94%94%EC%95%88-%EB%B2%A1%ED%84%B0-%EA%B0%84-%EB%B3%80%ED%99%98

        //���͸� ������ ��ȯ
        public static double VectorToDegree(Vector2 vector)
        {
            double radian = Math.Atan2(vector.y, vector.x);
            return (radian * 180.0 / Math.PI);
        }

        //���͸� �������� ��ȯ
        public static double VectorToRadian(Vector2 vector)
        {
            return Math.Atan2(vector.y, vector.x);
        }

        //������ ������ ��ȯ
        public static double RadianToDegree(double radian)
        {
            return (radian * 180.0 / Math.PI);
        }

        //������ �������� ��ȯ
        public static double DegreeToRadian(double degree)
        {
            return (Math.PI / 180.0) * degree;
        }





        //2���� ���� �ܳ��� https://shakddoo.tistory.com/entry/c-%EC%A0%90-%EC%84%A0%EB%B6%84-%EA%B0%84%EC%9D%98-%EA%B1%B0%EB%A6%AC-%EA%B5%AC%ED%95%98%EA%B8%B0?category=362471
        //2���� ���� ����
        public static float DotProduct(Vector2 left, Vector2 right)
        {
            return (float)(left.x * right.x + left.y * right.y);
        }

        // 2���� ���� ����
        public static float CrossProduct(Vector2 left, Vector2 right)
        {
            return (float)(left.x * right.y - left.y * right.x);
        }

        //���а� �� ������ �Ÿ�
        public static float DistanceLineAndPoint(Vector2 s, Vector2 e, Vector2 p)
        {
            Vector2 sp = p - s; Vector2 se = e - s; Vector2 es = s - e; Vector2 ep = p - e;

            if (SYMath.DotProduct(sp, se) * SYMath.DotProduct(es, ep) >= 0)
            {
                return Math.Abs(SYMath.CrossProduct(sp, se) / Vector2.Distance(s, e));
            }
            else
            {
                return Math.Min(Vector2.Distance(s, p), Vector2.Distance(e, p));
            }
        }




    }
}
