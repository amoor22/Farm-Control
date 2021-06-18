using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmerLoader : MonoBehaviour
{
    
    #region Variables
    public bool showPreview = false; public GameObject Farmer; bool showing = false;
    public LayerMask layerMask; public bool CanShow = false; public AnimalControl animalControl;
    public bool start; bool Done = false; int currentScore;
    #endregion

    #region Unity methods

    void Start()
    {
        
    }

    
    void Update()
    {
        if(CanShow){
            if(Input.GetKeyDown(KeyCode.Q)){ // && !EventSystem.current.IsPointerOverGameObject()
                showPreview = !showPreview;
                if(showing){
                    Farmer.SetActive(showPreview);
                }
            }
            if(showPreview){
                currentScore = animalControl.CurrentScore;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 100f, layerMask)){
                    if(!showing){
                        Farmer = Instantiate(Farmer);
                        Farmer.transform.position = hit.point;
                        showing = true;
                    }
                    Farmer.transform.LookAt(new Vector3(Camera.main.transform.position.x, Farmer.transform.position.y, Camera.main.transform.position.z));
                    Farmer.transform.position = hit.point;
                    // Quaternion TempRot = Farmer.transform.rotation;
                    // Farmer.transform.rotation = Quaternion.Euler(0f, TempRot.y, 0f);
                }
                if(Input.GetKeyDown(KeyCode.Space)){
                    // Farmer = Instantiate(Farmer);
                    // Farmer.transform.position = hit.point;
                    // showing = true;
                    if(currentScore - 50 >= 0){
                        // PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - 50);
                        animalControl.PurchaseFarmer();
                        showPreview = false;
                        CanShow = false;
                        Done = true;
                    }
                }
            }
        }
        if(Done){
            if(start){
                StartCoroutine(EndFarmer(5f));
                Done = false;
            }
        }
    }
    IEnumerator EndFarmer(float time){
        foreach(GameObject i in animalControl.Animals){
            i.GetComponent<SheepMovement>().isFarmer = true;
            i.GetComponent<SheepMovement>().Farmer = Farmer;
        }
        yield return new WaitForSeconds(time);
        foreach(GameObject i in animalControl.Animals){
            i.GetComponent<SheepMovement>().isFarmer = false;
        }
        Farmer.gameObject.SetActive(false);
    }
  
    #endregion
}
