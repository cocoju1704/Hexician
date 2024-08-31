using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
    public List<Monster> monsters;
    public List<bool> isTarget;
    
    [Header("설정")]
    [SerializeField]
    Vector3 startPos = new Vector3(0, 8f, 0);

    [Header("필수 사전 설정")]
    [SerializeField]
    GameObject statusUIPrefab;
    [SerializeField] 
    GameObject arrowObj;

    public void Init(List<GameObject> monsterPrefabs) {
        int count = monsterPrefabs.Count;

        monsters = new List<Monster>();
        isTarget = new List<bool>();
        
        if(count == 1) {
            SpawnMonster(monsterPrefabs[0], startPos);
        }
        
        Vector3 pos;
        float xOffset = 3f, val, xPos;
        for(int i = 0; i < count; i++) {
            val = i / ((float) count - 1f);

            xPos = Mathf.Lerp(count * -xOffset, count * xOffset, val);
            pos = startPos + new Vector3(xPos, 0, 0);
            Debug.Log(monsterPrefabs.Count);
            SpawnMonster(monsterPrefabs[i], pos);
        }
    }

    public void SetTarget(Monster monster) {
        int count = monsters.Count;
        
        for(int i = 0; i < count; i++) {
            if(monster == monsters[i]) {
                isTarget[i] = true;
                MoveArrow(monster.transform.position);
            }
            else {
                isTarget[i] = false;
            }
        }
    }

    public Unit GetTarget() {
        int count = monsters.Count;
        
        for(int i = 0; i < count; i++) {
            if(isTarget[i]) {
                return monsters[i];
            }
        }

        SetTarget(monsters[0]);
        return monsters[0];
    }

    public Unit GetRandomTarget() {
        int count = monsters.Count;
        int randNum = Random.Range(0, count);

        return monsters[randNum];
    }

    public List<Unit> GetAllTargets() {
        return monsters.ConvertAll(monster => monster as Unit).ToList();
    }

    void SpawnMonster(GameObject prefab, Vector3 pos) {
        Monster monster = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Monster>();
        monsters.Add(monster);
        isTarget.Add(false);

        GameObject cavasObj = GameObject.Find("Canvas");
        Vector3 statusPos = Camera.main.WorldToScreenPoint(pos - new Vector3(0, 3, 0));
        StatusUI status = Instantiate(statusUIPrefab, statusPos, Quaternion.identity).GetComponent<StatusUI>();
        status.transform.SetParent(cavasObj.transform);

        status.Init(monster);
    }

    public void StartAction() {
        StartCoroutine(StartActionCo());
    }

    IEnumerator StartActionCo() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
        
        yield return waitForSeconds;

        foreach(Monster monster in monsters) {
            monster.Do();
            yield return waitForSeconds;
        }

        yield return waitForSeconds;

        // 모든 행동이 끝나면
        BattleManager.instance.PassTurn(isPlayerTurn: false);
    }

    void MoveArrow(Vector3 targetPosition) {
        if(arrowObj.activeSelf == false) {
            arrowObj.SetActive(true);
        }

        float xPos = Camera.main.WorldToScreenPoint(targetPosition).x;
        float yPos = arrowObj.transform.position.y;

        arrowObj.transform.position = new Vector3(xPos, yPos, 1);
    }
}
