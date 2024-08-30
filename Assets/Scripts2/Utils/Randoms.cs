
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Randoms {
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
    public static Vector3Int SelectRandomTile(List<Vector3Int> keys) {
        return keys[UnityEngine.Random.Range(0, keys.Count)];
    }
}
