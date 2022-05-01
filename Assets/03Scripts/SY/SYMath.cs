using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//전세윤, 나만의 유틸리티 함수를 모아놓은 것 입니다. https://www.youtube.com/watch?v=997G_JrJ7iA

namespace SYMath
{
    public static class SYMath
    {

        //각도, 벡터, 라디안 변경 https://shakddoo.tistory.com/entry/c-%EA%B0%81%EB%8F%84-%EB%9D%BC%EB%94%94%EC%95%88-%EB%B2%A1%ED%84%B0-%EA%B0%84-%EB%B3%80%ED%99%98

        //벡터를 각도로 변환
        public static double VectorToDegree(Vector2 vector)
        {
            double radian = Math.Atan2(vector.y, vector.x);
            return (radian * 180.0 / Math.PI);
        }

        //벡터를 라디안으로 변환
        public static double VectorToRadian(Vector2 vector)
        {
            return Math.Atan2(vector.y, vector.x);
        }

        //라디안을 각도로 변환
        public static double RadianToDegree(double radian)
        {
            return (radian * 180.0 / Math.PI);
        }

        //각도를 라디안으로 변환
        public static double DegreeToRadian(double degree)
        {
            return (Math.PI / 180.0) * degree;
        }





        //2차원 벡터 외내적 https://shakddoo.tistory.com/entry/c-%EC%A0%90-%EC%84%A0%EB%B6%84-%EA%B0%84%EC%9D%98-%EA%B1%B0%EB%A6%AC-%EA%B5%AC%ED%95%98%EA%B8%B0?category=362471
        //2차원 벡터 내적
        public static float DotProduct(Vector2 left, Vector2 right)
        {
            return (float)(left.x * right.x + left.y * right.y);
        }

        // 2차원 벡터 외적
        public static float CrossProduct(Vector2 left, Vector2 right)
        {
            return (float)(left.x * right.y - left.y * right.x);
        }

        //선분과 점 사이의 거리
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
