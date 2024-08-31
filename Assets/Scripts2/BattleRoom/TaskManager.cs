using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour {
    public Register register;

    LinkedList<BattleTask> queue;
    public List<Hex> hexesToDestroy;

    public UnityEvent<BattleTask> OnStartBattle;
    public UnityEvent<BattleTask> OnStartTurn;
    public UnityEvent<BattleTask> OnEndTurn;
    public UnityEvent<BattleTask> OnEndBattle;
    public UnityEvent<BattleTask> OnAttack;
    public UnityEvent<BattleTask> OnHeal;
    public UnityEvent<BattleTask> OnMountHex;
    public UnityEvent<BattleTask> OnCreatHex;
    public UnityEvent<BattleTask> OnDismountHex;
    public UnityEvent<BattleTask> OnDestroyHex;

    bool isExecuting;
    void Awake() {
        queue = new LinkedList<BattleTask>();
        hexesToDestroy = new List<Hex>();
        register = new Register();

        OnStartBattle = new UnityEvent<BattleTask>();
        OnStartTurn = new UnityEvent<BattleTask>();
        OnEndTurn = new UnityEvent<BattleTask>();
        OnEndBattle = new UnityEvent<BattleTask>();
        OnAttack = new UnityEvent<BattleTask>();
        OnHeal = new UnityEvent<BattleTask>();
        OnMountHex = new UnityEvent<BattleTask>();
        OnCreatHex = new UnityEvent<BattleTask>();
        OnDismountHex = new UnityEvent<BattleTask>();
        OnDestroyHex = new UnityEvent<BattleTask>();

        isExecuting = false;
    }

    public void Add(BattleTask task, Register register = null) {
        Debug.Log("Add: " + task.ToString());

        if(register != null) {
            task.SetRegister(register);
        }
        
        queue.AddLast(task);

        if(!isExecuting) {
            isExecuting = true;
            ExecuteTasks(true);
            isExecuting = false;
        }
    }

    public void Add(List<BattleTask> tasks, Register register = null) {
        Debug.Log("Add: " + tasks.Count + "개");
        foreach(BattleTask task in tasks) {
            if(register != null) {
                task.SetRegister(register);
            }

            queue.AddLast(task);
        }

        if(!isExecuting) {
            isExecuting = true;
            ExecuteTasks(true);
            isExecuting = false;
        }
    }

    void ExecuteTasks(bool isFirst) {
        Debug.Log(executeTasksCount++ + "번째 ExecuteTasks 시작: " + ConvertQueueToStr());

        LinkedList<BattleTask> queueCaptured = queue;
        queue = new LinkedList<BattleTask>();

        foreach(BattleTask task in queueCaptured) {
            NotifyPreEvent(task);
            Debug.Log("작동 전: " + task + " | 트리거 됨 : " + ConvertQueueToStr());
            if(queue.Count > 0) {
                ExecuteTasks(false);
            }

            task.Execute();
            NotifyPostEvent(task);

            Debug.Log("작동 후: " + task + " | 트리거 됨 : " + ConvertQueueToStr());
            if(queue.Count > 0) {
                ExecuteTasks(false);
            }
            

            if(isFirst) {
                Debug.Log("isFirst, 처리할 코어 수 : " + hexesToDestroy.Count);
                while(hexesToDestroy.Count > 0) {
                    foreach(Hex hex in hexesToDestroy) {
                        hex.DeactivateAura();
                        Add(new DestroyHexTask(hex));
                    }
                    hexesToDestroy.Clear();

                    ExecuteTasks(false);
                }
            }
        }
    }

    void NotifyPreEvent(BattleTask task) {
        switch(task) {
            case StartBattleTask :
                OnStartBattle.Invoke(task);
                break;
            case StartTurnTask :
                OnStartTurn.Invoke(task);
                break;
            case EndTurnTask :
                OnEndTurn.Invoke(task);
                break;
            case EndBattleTask :
                OnEndBattle.Invoke(task);
                break;
            case DestroyHexTask:
                OnDestroyHex.Invoke(task);
                break;
            default:
                break;
        }
    }

    void NotifyPostEvent(BattleTask task) {
        switch(task) {
            case MountHexTask :
                OnMountHex.Invoke(task);
                break;
            case DamageTask :
                OnAttack.Invoke(task);
                break;
            case HealTask :
                OnHeal.Invoke(task);
                break;
            default:
                break;
        }
    }

    #region Debug
    int executeTasksCount = 0;
    string ConvertQueueToStr() {
        string str = "";
        foreach(BattleTask task1 in queue) {
            str += task1.ToString() + ", ";
        }

        return str;
    }
    #endregion
}
