using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneLoaderMenu : MonoBehaviour
{
    public int lastPlayedLevel; public TextMeshProUGUI highScoreText;
    public void LoadFirstScene(){
        lastPlayedLevel = PlayerPrefs.GetInt("lastLevel", 1);
        if(lastPlayedLevel == 0){
            PlayerPrefs.SetInt("lastLevel", 1);
            lastPlayedLevel = 1;
        }
        SceneManager.LoadScene(lastPlayedLevel);
    }
    public void UpdateHighScore(){
        highScoreText.text = string.Format("HighScore : {0}", PlayerPrefs.GetInt("score", 0));
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void ResetAll(){
        PlayerPrefs.DeleteAll();
    }

}
