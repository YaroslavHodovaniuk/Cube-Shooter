using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Don't specifically need anything here other than the fact it's persistent.
/// I like to keep one main object which is never killed, with sub-systems as children.
/// </summary>
public class Systems : PersistentSingleton<Systems>
{
    [SerializeField] private LevelData levelData;

    public LevelData LevelData 
    {
        get 
        { 
            return levelData;
        } 
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
