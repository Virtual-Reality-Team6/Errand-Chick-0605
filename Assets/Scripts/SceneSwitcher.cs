using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public SceneLoader sceneLoader;
    
    Player enterPlayer;

    public void SwitchToNextScene()
    {
        // 다음 씬으로 전환
        sceneLoader.LoadScene("NextScene");
    }
}
