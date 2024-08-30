using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadSystem : Singleton<SceneLoadSystem> {
    public UnityEvent OnFinishTransition;
    
    [SerializeField]GameObject fadingBlack;
    [SerializeField, Range(0, 1)] float fadeOutTime;
    [SerializeField, Range(0, 0.5f)] float fadeInTime;

    CanvasGroup cgFadeBlack;
    Image imageFadeBlack;

    protected override void Awake() {
        base.Awake();

        OnFinishTransition = new UnityEvent();
        cgFadeBlack = fadingBlack.GetComponent<CanvasGroup>();
        imageFadeBlack = fadingBlack.GetComponent<Image>();
    }

    Dictionary<Enums.RoomType, string> sceneNameDict = new Dictionary<Enums.RoomType, string>(){
        {Enums.RoomType.Empty, "BattleScene"},
        {Enums.RoomType.Elite, "BattleScene"},
        {Enums.RoomType.Boss, "BattleScene"},
        {Enums.RoomType.Danger, "BattleScene"},
        {Enums.RoomType.Chest, "ChestScene"},
        {Enums.RoomType.Shop, "ShopScene"},
        {Enums.RoomType.Encounter, "EncounterScene"},
        {Enums.RoomType.Stair, "StairScene"},
        {Enums.RoomType.Debug, "DebugScene"},
    };

    public void LoadScene(Enums.RoomType roomType) {
        LoadScene(sceneNameDict[roomType]);
    }

    public void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneCo(sceneName));
    }

    IEnumerator LoadSceneCo(string sceneName) {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        cgFadeBlack.blocksRaycasts = true;

        yield return PlayFadeOutAnim().WaitForCompletion();
        
        while(!asyncOperation.isDone) {
            if(asyncOperation.progress >= 0.9f) {
                break;
            }
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        yield return PlayFadeInAnim().WaitForCompletion();
        cgFadeBlack.blocksRaycasts = false;

        OnFinishTransition.Invoke();
        OnFinishTransition.RemoveAllListeners();
    }
    public void LoadStageScene() {
        LoadScene("StageScene");
    }
    Tweener PlayFadeOutAnim() {
        return imageFadeBlack.DOFade(1, fadeOutTime);
    }
    Tweener PlayFadeInAnim() {
        Tweener tweener = imageFadeBlack.DOFade(0, fadeInTime);
        return tweener;
    }
}
