// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;

// public static class StageAreas
// {
//     public static List<Vector3Int> firstQuadrant = new List<Vector3Int> {
//         new Vector3Int(2, -2, 0),
//         new Vector3Int(3, -2, -1),
//         new Vector3Int(4, -2, -2),
//         //new Vector3Int(5, -2, -3),
//         new Vector3Int(3, -3, 0),
//         new Vector3Int(4, -3, -1),
//         new Vector3Int(5, -3, -2),
//         //new Vector3Int(6, -3, -3),
//         new Vector3Int(2, -1, -1),
//         new Vector3Int(3, -1, -2),
//         new Vector3Int(4, -1, -3),
//     };
//     public static List<Vector3Int> secondQuadrant = new List<Vector3Int> {
//         new Vector3Int(0, -2, 2),
//         new Vector3Int(-1, -2, 3),
//         new Vector3Int(-2, -2, 4),
//         //new Vector3Int(-3, -2, 5),
//         new Vector3Int(0, -3, 3),
//         new Vector3Int(-1, -3, 4),
//         new Vector3Int(-2, -3, 5),
//         //new Vector3Int(-3, -3, 6),
//         new Vector3Int(-1, -1, 2),
//         new Vector3Int(-2, -1, 3),
//         new Vector3Int(-3, -1, 4),
//     };
//     public static List<Vector3Int> thirdQuadrant = new List<Vector3Int> {
//         new Vector3Int(-2, 2, 0),
//         new Vector3Int(-3, 2, 1),
//         new Vector3Int(-4, 2, 2),
//         //new Vector3Int(-5, 2, 3),
//         new Vector3Int(-3, 3, 0),
//         new Vector3Int(-4, 3, 1),
//         new Vector3Int(-5, 3, 2),
//         //new Vector3Int(-6, 3, 3)
//         new Vector3Int(-2, 1, 1),
//         new Vector3Int(-3, 1, 2),
//         new Vector3Int(-4, 1, 3),
//     };
//     public static List<Vector3Int> fourthQuadrant = new List<Vector3Int> {
//         new Vector3Int(0, 2, -2),
//         new Vector3Int(1, 2, -3),
//         new Vector3Int(2, 2, -4),
//         //new Vector3Int(3, 2, -5),
//         new Vector3Int(0, 3, -3),
//         new Vector3Int(1, 3, -4),
//         new Vector3Int(2, 3, -5),
//         //new Vector3Int(3, 3, -6),
//         new Vector3Int(1, 1, -2),
//         new Vector3Int(2, 1, -3),
//         new Vector3Int(3, 1, -4),
//     };
//     public static List<List<Vector3Int>> shopAreas = new List<List<Vector3Int>> {
//         firstQuadrant,
//         secondQuadrant,
//         thirdQuadrant,
//         fourthQuadrant
//     };
//     public static List<Vector3Int> upgradeArea = new List<Vector3Int> {
//         new Vector3Int(0, 0, 0),
//         new Vector3Int(1, -2, 1),
//         new Vector3Int(-1, 2, -1),
//     };
// }