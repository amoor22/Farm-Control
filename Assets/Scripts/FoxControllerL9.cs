using System.Collections.Generic;
using UnityEngine;

public class FoxControllerL9 : MonoBehaviour
{
    
    #region Variables
    public List<GameObject> Foxes = new List<GameObject>();
    GameObject current; public bool BackToPlace = true; int first = 0; int prev;
    #endregion

    #region Unity methods

    
    
    void Update()
    {
        if(Foxes.Count > 0){
            if(BackToPlace){
                int currentIndex = Random.Range(0, Foxes.Count);
                // while(currentIndex == prev){
                    // currentIndex = Random.Range(0, Foxes.Count);
                // }
                for(int i = 0; i < 100; i++){
                    if(currentIndex != prev){
                        break;
                    }
                    else{
                        currentIndex = Random.Range(0, Foxes.Count);
                    }
                }
                current = Foxes[currentIndex];
                if(current.GetComponent<FoxScript>().AdvancedMove){
                    prev = currentIndex;
                    current.GetComponent<FoxScript>().MoveAdvanced = true;
                    current.GetComponent<FoxScript>().timeToNext = Time.time;
                    BackToPlace = false;
                    if(first == 0){
                        current.GetComponent<FoxScript>().MoveRounds++;
                    }
                    first++;
                }  
            }
        }
    }
  
    #endregion
}
