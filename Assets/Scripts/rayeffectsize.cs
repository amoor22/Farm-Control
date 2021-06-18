using UnityEngine;

public class rayeffectsize : MonoBehaviour
{
    
    public GameObject fencePlacer;
    void Update()
    {
        if (!(transform.localScale.x >= 10f)){
            transform.localScale = transform.localScale + Vector3.one * 50f * Time.deltaTime;
        }
        else{
            fencePlacer.GetComponent<FencePlacerScript>().BubbleShoot = false;
            Destroy(gameObject, 0.2f);
        }
    }
  
}
