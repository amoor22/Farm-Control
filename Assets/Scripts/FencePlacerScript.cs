using UnityEngine;
using TMPro;

public class FencePlacerScript : MonoBehaviour
{
    
    #region Variables
    public bool showPreview = false; public GameObject Fence;
    public float rotation = 0f;
    public LayerMask layerMask;
    public Material greenMaterial; public Material originalMaterial;
    bool beingPreviewed; bool BoxColliderOn = true;
    public int remaining; public TextMeshProUGUI remainingText;
    public bool BubbleShoot = false;
    #endregion

    #region Unity methods

    void Start()
    {

    }

    
    void Update()
    {
        if(remaining > 0){
            if(Input.GetKeyDown(KeyCode.E)){
                showPreview = !showPreview;
            }else if(Input.GetKeyDown(KeyCode.R)){
                rotation += 45f;
            }
            if (showPreview && !BubbleShoot){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Quaternion TempRotation = transform.rotation;
                if(Physics.Raycast(ray, out hit, 150f, layerMask)){
                    if (!beingPreviewed){
                        Fence = Instantiate(Fence, hit.point, Quaternion.Euler(TempRotation.x, rotation, 0f));
                        Fence.SetActive(true);
                        BoxColliderOn = true;
                        Fence.GetComponent<Renderer>().material = greenMaterial;
                        beingPreviewed = true;
                    }
                    else{
                        if (BoxColliderOn){
                            Fence.GetComponent<BoxCollider>().enabled = false;
                        }
                        if(Input.GetMouseButtonDown(1)){
                            remaining--;
                            remainingText.text = string.Format("Fences : {0}", remaining);
                            Fence.GetComponent<Renderer>().material = originalMaterial;
                            Fence.GetComponent<BoxCollider>().enabled = true;
                            if (remaining > 0){
                                Fence = Instantiate(Fence, hit.point, Quaternion.Euler(TempRotation.x, rotation, 0f));
                                Fence.GetComponent<Renderer>().material = greenMaterial;
                            }
                        }
                        Fence.transform.position = hit.point;
                        Fence.transform.rotation = Quaternion.Euler(TempRotation.x, rotation, 0f);
                    }
                }
            }
            else{
                Fence.SetActive(false);
                beingPreviewed = false;
            }
        }
        
    }
  
    #endregion
}
