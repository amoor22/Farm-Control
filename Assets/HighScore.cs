using UnityEngine;
using TMPro;
public class HighScore : MonoBehaviour
{
    void Start(){
        GetComponent<TextMeshProUGUI>().text = string.Format("Highscore : {0}",PlayerPrefs.GetInt("score", 0)) ;
    }
}
