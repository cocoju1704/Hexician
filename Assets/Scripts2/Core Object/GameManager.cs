using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [HideInInspector] public Player p;
    [HideInInspector] public Stage stage;
    [HideInInspector] public ItemEvents itemVariables;
    [HideInInspector] public ItemActions itemActions;

    [SerializeField] Player playerPrefab;
    [SerializeField] Stage stagePrefab;
    [SerializeField] ItemEvents itemVariablesPrefab;
    [SerializeField] ItemActions itemActionsPrefab;

    public void InitGame() {
        p = Instantiate(playerPrefab, transform);
        p.name = "Player";
        stage = Instantiate(stagePrefab, transform);
        stage.name = "Stage";
        itemVariables = Instantiate(itemVariablesPrefab, transform);
        itemVariables.name = "ItemVariables";
        itemActions = Instantiate(itemActionsPrefab, transform);
        itemActions.name = "ItemActions";
    }

    public Room GetCurrentRoom() {
        Vector3Int playerCoordinate = p.coordinate;
        return stage.GetRoom(playerCoordinate);
    }
    public Room GetMidRoom() {
        Vector3Int playerCoordinate = Vector3Int.zero;
        return stage.GetRoom(playerCoordinate);
    }
}
