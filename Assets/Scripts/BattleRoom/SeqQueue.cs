using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SeqQueue : MonoBehaviour {
    LinkedList<Tween> queue;

    void Start() {
        queue = new LinkedList<Tween>();
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim() {
        WaitForSeconds waitForSec = new WaitForSeconds(0.1f);
      
        while(true) {
            if(queue.First is not null) {
                Tween anim = queue.First.Value;
                queue.RemoveFirst();
                anim.Restart();

                yield return anim.WaitForCompletion();
            }
            else {
                yield return waitForSec;
            }
        }
    }

    public void Add(Tween anim) {
        queue.AddLast(anim.Pause());
    }

}
    