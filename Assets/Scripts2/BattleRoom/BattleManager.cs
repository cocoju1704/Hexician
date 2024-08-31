using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


// 게임 관리, UI, 턴 관리 등등을 관리하는데 추후 정리할 예정
public class BattleManager : MonoBehaviour {
    static BattleManager _instance;
    public static BattleManager instance {
        get {
            if (_instance == null) {
                _instance = (BattleManager)FindObjectOfType(typeof(BattleManager));
            }

            return _instance;
        }
    }

    
    public int drawCount;

    public Player p;
    public BattleBoard board;
    public CardManager cardManager;
    public TaskManager taskManager;
    public SeqQueue seqQueue;
    

    PlayerControl pControl;
    public MonsterManager monsterManager;


    [Header("추후 제거 예정")]
    public List<GameObject> monsterPrefabs;
    public PlayerStatusUI playerStatusUI;

    // bool isPlayerTurn;

    [Header("필수 사전 설정")]
    [SerializeField]
    Button endButton;

    void Awake() {
        if(_instance == null) {
            _instance = this;
        }
        else if(_instance != null) {
            Destroy(gameObject);
            return;
        }
        p = GameManager.instance.p.GetComponent<Player>();
        pControl = GameObject.Find("PlayerControl").GetComponent<PlayerControl>();
        board = GameObject.Find("Board").GetComponent<BattleBoard>();
        taskManager = GetComponent<TaskManager>();
        seqQueue = GetComponent<SeqQueue>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
    }

    void Start() {
        SetupBattle();

        AddTask(new List<BattleTask> {
            new StartBattleTask(),
            new StartTurnTask(isPlayerTurn: true, drawCount),
        });
    }

    void SetupBattle() {
        board.Init(p.activatedTiles);

        // 플레이어가 가진 기어를 객체화
        List<Hex> playersHexs = ResourceSystem.instance.MakeHex(p.hexIds);
        cardManager.Init(playersHexs);

        // 몬스터 생성
        monsterManager.Init(monsterPrefabs);

        playerStatusUI.Init(p);
        endButton.enabled = false;
        endButton.onClick.AddListener(() => {
            PassTurn(isPlayerTurn: true);
        });
    }

    public void PassTurn(bool isPlayerTurn) {
        if(isPlayerTurn) {
            AddTask(new List<BattleTask>() {
                new EndTurnTask(isPlayerTurn: true),
                new StartTurnTask(isPlayerTurn: false)
            });
        }
        else {
            AddTask(new List<BattleTask>() {
                new EndTurnTask(isPlayerTurn: false),
                new StartTurnTask(isPlayerTurn: true, drawCount)
            });
        }
    }

    #region PlayerControl
    public void MakePlayerCanContol() {
        Debug.Log("플레이어 컨트롤 가능");
        pControl.canAct = true;
        pControl.canOver = true;
        endButton.enabled = true;
    }

    public void MakePlayerCantContol() {
        Debug.Log("플레이어 컨트롤 불가");
        pControl.canAct = false;
        pControl.canOver = false;
        endButton.enabled = false;
    }
    #endregion

    #region TaskManager
    public void AddTask(BattleTask task, Register register = null) {
        taskManager.Add(task, register);
    }
    public void AddTask(List<BattleTask> tasks, Register register = null) {
        taskManager.Add(tasks, register);
    }
    #endregion

    #region SeqQueue
    public void AddSeq(Tween anim) {
        seqQueue.Add(anim);
    }
    #endregion

    #region MonsterManager
    public void StartMonstersAction() {
        monsterManager.StartAction();
    }

    public void SetTarget(Monster monster) {
        monsterManager.SetTarget(monster);
        taskManager.register.target = monster;
    }
    public Unit GetTarget(){
        return monsterManager.GetTarget();
    }
    public Unit GetRandomTarget() {
        return monsterManager.GetRandomTarget();
    } 

    public List<Unit> GetAllTargets() {
        return monsterManager.GetAllTargets();
    }
    #endregion
}
