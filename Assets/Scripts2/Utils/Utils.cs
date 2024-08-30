
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Utils {
    public static Vector3Int[] Vectors = new Vector3Int[6] {
        new Vector3Int( 0, 1, -1 ),
        new Vector3Int( -1, 1, 0 ),
        new Vector3Int( -1, 0, 1 ),
        new Vector3Int( 0, -1, 1 ),
        new Vector3Int( 1, -1, 0 ),
        new Vector3Int( 1, 0, -1 ),
    };

    public static Vector3 ConvertToPosition(Vector3Int coordinate, float size) {
        float x = coordinate.x;
        float y = coordinate.y;

        float retX = size * (float) Math.Sqrt(3) * (x + y / 2) + 0.001f;
        float retY = size * y  * ((float) 3 / 2) + 0.001f;  

        return new Vector3(retX, -retY, 0);
    }

    public static bool IsAdjacent(Vector3Int a, Vector3Int b) {
        if (GetDistance(a, b) == 0) return false;
        if (GetDistance(a, b) < 2) return true;
        return false;
    }
    // public static bool IsAdjacent(LegacyStageTile mouseOverTile, LegacyStageTile playerTile) {
    //     Vector3Int mouseOverPos = mouseOverTile.coordinate;
    //     Vector3Int playerPos = playerTile.coordinate;
    //     return IsAdjacent(mouseOverPos, playerPos);
    // }
    // // 두 타일 간 거리 구하는 함수
    // public static int GetDistance(LegacyStageTile a, LegacyStageTile b) {
    //     Vector3Int aPos = a.coordinate;
    //     Vector3Int bPos = b.coordinate;
    //     return GetDistance(aPos, bPos);
    //     //거리가 2면 인접한 타일이다.
    // }
    public static bool IsAdjacentFront(Vector3Int a, Vector3Int b) {
        Vector3Int dif = a - b;
        if (dif == 
        Vectors[(int) Enums.Direction.BottomLeft] ||
        dif == Vectors[(int) Enums.Direction.TopLeft] ||
        dif == Vectors[(int) Enums.Direction.Left]) {
            return true;
        }
        return false;
    }
    public static int GetDistance(Vector3Int a, Vector3Int b) {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 2;
    }
    public static int GetDistFromCenter(Vector3Int a) {
        return Math.Max(Math.Abs(a.x), Math.Max(Math.Abs(a.y), Math.Abs(a.z)));
    }
    public static Vector3 GetSize(GameObject obj) {
        BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
        Vector3 size = collider.bounds.size;
        return size;
    }

    public static IEnumerator WaitForSec(float sec, UnityAction callback) {
        yield return new WaitForSeconds(sec);
        callback();
    }

    public static void Shuffle<T>(this IList<T> list) {
        int count = list.Count;
        T temp;
        for(int i = 0; i < list.Count - 1; i++) {
            int r = UnityEngine.Random.Range(i, count);

            temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }

    public static bool Compare(int a, Enums.ComparisonOperator op, int b) {
        if(op == Enums.ComparisonOperator.EQ) {
            return a == b;
        }
        else if(op == Enums.ComparisonOperator.NE) {
            return a != b;
        }
        else if(op == Enums.ComparisonOperator.LT) {
            return a < b;
        }
        else if(op == Enums.ComparisonOperator.LE) {
            return a <= b;
        }
        else if(op == Enums.ComparisonOperator.GT) {
            return a > b;
        }
        else if(op == Enums.ComparisonOperator.GE) {
            return a >= b;
        }

        return false;
    }

    public static int Cacluate(int a, Enums.MathOperator op, int b) {
        if(op == Enums.MathOperator.Plus) {
            return a + b;
        }
        else if(op == Enums.MathOperator.Minus) {
            return a - b;
        }
        else if(op == Enums.MathOperator.Mul) {
            return a * b;
        }
        else if(op == Enums.MathOperator.Div) {
            return a / b;
        }
        else if(op == Enums.MathOperator.Percent) {
            return (a * b) / 100;
        }

        return 0;
    }
    
    public static int GetWeightedRandomOption(List<int> weights) {
        int sum = 0;
        foreach(int weight in weights) {
            sum += weight;
        }

        int rand = UnityEngine.Random.Range(0, sum);
        int acc = 0;
        for(int i = 0; i < weights.Count; i++) {
            acc += weights[i];
            if(rand < acc) {
                return i;
            }
        }

        return 0;

    }
}
