using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] Button exitButton;
    public void Awake() {
        exitButton.onClick.AddListener(ExitRoom);
    }
    
    public void ExitRoom() {
        SceneLoadSystem.instance.LoadStageScene();
    }
}
