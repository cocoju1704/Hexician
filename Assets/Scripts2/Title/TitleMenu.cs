using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {
    [SerializeField] Button continueButton;
    [SerializeField] GameObject selectionObj; 
    
    Enums.CharacterType characterType;
    Player player;
    Stage stage;

    void Start() {
        if(SaveSystem.instance.HasSaveFile() == false) {
            continueButton.interactable = false;
        }

        player = FindObjectOfType<Player>();
        stage = FindObjectOfType<Stage>();
    }
    
    public void OpenCharacterSelection() {
        selectionObj.SetActive(true);
    }

    public void CloseCharacterSelection() {
        characterType = Enums.CharacterType.None;
        selectionObj.SetActive(false);
    }

    public void StartNewGame() {
        if(characterType == Enums.CharacterType.None) {
            characterType = Enums.CharacterType.Fire;
        }
        PlayerData playerData = ResourceSystem.instance.GetPlayerData(characterType);
        
        GameManager.instance.InitGame();
        player = GameManager.instance.p;
        stage = GameManager.instance.stage;

        player.SetPlayer(playerData);
        stage.CreateStage();

        SceneLoadSystem.instance.LoadScene("StageScene");
    }

    public void ContinueGame() {
        GameManager.instance.InitGame();

        SaveSystem.instance.LoadFile();

        player = GameManager.instance.p;
        stage = GameManager.instance.stage;

        SceneLoadSystem.instance.LoadScene("StageScene");

        // Room currRoom = GameManager.instance.GetCurrentRoom();

        // if(currRoom.isVisited) {
        //     SceneLoadSystem.instance.ChangeScene("StageScene");
        // }
        // else {
        //     SceneLoadSystem.instance.ChangeScene(currRoom.roomType);
        // }
    }

    public void QuitGame() {
        Debug.Log("게임 종료");
        // Application.Quit();
    }

    public void SelectCharacter(Enums.CharacterType characterType) {
        this.characterType = characterType;
    }
}