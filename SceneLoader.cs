using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public CharacterLoader characterLoader;
    [SerializeField] private string sceneCreate = "CharacterCreation";
    [SerializeField] private string sceneGame = "Summon 1";
    void Start()
    {
        characterLoader = GetComponent<CharacterLoader>();
    }
    public void OnClickCreate()
    {
        SceneManager.LoadScene(sceneCreate, LoadSceneMode.Single);
    }
    public void OnClickPlay()
    {
        if (characterLoader != null)
        {
            Customiser currentCustomiser = characterLoader.GetCurrentCustomiser();
            if (currentCustomiser != null)
            {
                characterLoader.SaveCustomisation(currentCustomiser);
            }
            else
            {
                Debug.LogError("“екущий кастомизатор не найден!");
            }
        }
        else
        {
            Debug.LogError("CharacterLoader не найден!");
        }

        SceneManager.LoadScene(sceneGame, LoadSceneMode.Single);
    }
}
