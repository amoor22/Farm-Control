using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class SheepMovement : MonoBehaviour
{
    
    #region Variables
    public GameObject clickObject; public Camera MainCamera; public TextMeshProUGUI ScoreText;
    public string type; public GameObject fencePlacer;
    AnimalControl animalControl;
    Vector3 direction;
    Quaternion rotation;
    Rigidbody rb;
    float deltaTime;
    Animator animator; public bool isFarmer; public GameObject Farmer;
    public float speed = 5f; public bool play = false; 
    FireSize fireSize;
    #endregion

    #region Unity methods

    void Start()
    {
        // SheepMovement special = new SheepMovement(); 
        animalControl = GameObject.Find("AnimalControl").GetComponent<AnimalControl>();
        animalControl.Animals.Add(gameObject);
        animator = GetComponent<Animator>();
        rotation = transform.rotation;
        transform.rotation = Quaternion.Euler(rotation.x, Random.Range(-180f, 180f), rotation.z);
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if(play){
            if(isFarmer){
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Farmer.transform.transform.position.x, transform.position.y, Farmer.transform.position.z), 3f * Time.fixedDeltaTime);
            }
            if(type == "bunny"){
                bunnySpecial();
            }else if(type == "sheep"){
                sheepSpecial();
            }
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // float Angle = AngleBetweenTwoPoints(mousePos, MainCamera.transform.position);
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hitInfo, 100f) && hitInfo.collider.tag != "ClickEffect"){
                    fencePlacer.GetComponent<FencePlacerScript>().BubbleShoot = true;
                    Instantiate(clickObject, hitInfo.point, transform.rotation);
                }
            }
            deltaTime = Time.fixedDeltaTime;
            transform.Translate(new Vector3(0f, 0f, speed * deltaTime));
        }
    }
    private void OnTriggerEnter(Collider other) {
        // transform.rotation = Quaternion.LookRotation(-other.transform.position.normalized);
        if (other.CompareTag("Barn")){
            animalControl.PlaySoundAlive();
            animalControl.ChangeAlive();
            animalControl.ChangeScore(true);   
            animalControl.Animals.Remove(gameObject);     
            animalControl.CheckEnd();
            gameObject.SetActive(false);
        }else {
            if(!other.CompareTag("Enemy") && !other.CompareTag("Water")){
                transform.LookAt(other.transform.position);
                Quaternion rotation = transform.rotation;
                rotation.x = 0f; rotation.z = 0f;
                transform.rotation = rotation;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        if(other.CompareTag("Water")){
            StartCoroutine(DeactiveAfter(1.5f));
            animalControl.PlaySoundDeath();
            animalControl.Animals.Remove(gameObject);
            animalControl.ChangeScore(false);
            animalControl.CheckEnd();
        }
    }
    
  
    #endregion
    IEnumerator DeactiveAfter(float time){
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b){
        return Mathf.Atan2(a.y - b.y, a.x - b.x);
    }
    void bunnySpecial(){
        if(Random.Range(0f, Time.deltaTime) >= Time.deltaTime * 0.98f){
            transform.Rotate(0f, Random.Range(-60f, 60f), 0f);
        }
        animator.Play("Jump");
    }
    void sheepSpecial(){
        if(Random.Range(0f, Time.deltaTime) >= Time.deltaTime * 0.99f){
            transform.Rotate(0f, Random.Range(-20f, 20f), 0f);
            animator.Play("Jump");
        }
        animator.Play("Run");
    }
}
