using UnityEngine;
using TMPro;
public class SceneBegin : MonoBehaviour
{
    
    #region Variables
    public int RemainingFencecsInt; public bool isFarmer; public bool CanFarmer;
    public TextMeshProUGUI RemainingFencecs;
    public TextMeshProUGUI Score;
    #endregion

    #region Unity methods

    void Start()
    {
        RemainingFencecs.text = string.Format("Fences : {0}", RemainingFencecsInt);
        Score.text = 0.ToString();
        GameObject.Find("FencePlacer").GetComponent<FencePlacerScript>().remaining = RemainingFencecsInt;
        if(CanFarmer){
            if(isFarmer){
                GameObject.Find("FarmerLoader").GetComponent<FarmerLoader>().CanShow = true;
            }else{
                GameObject.Find("FarmerLoader").GetComponent<FarmerLoader>().CanShow = false;
            }
        }
    }

    
  
    #endregion
}
