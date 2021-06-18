using UnityEngine;
using System.Collections;
public class DogMovement : MonoBehaviour
{
    
    #region Variables
    public float scale = 5f;
    AnimalControl animalControl; Vector3 originalPos; public float speed = 5f;
    Animator animator; public bool start;
    bool OnGround = false;
    #endregion

    #region Unity methods

    void Start()
    {
        
        // if(MoveRounds % 2 != 0){
        //     MoveRounds++;
        // }
        animator = GetComponent<Animator>();
        animalControl = GameObject.Find("AnimalControl").GetComponent<AnimalControl>();
        animalControl.HarmAnimalsDog.Add(gameObject);
        Vector3 TempScale = GetComponentInChildren<CapsuleCollider>().transform.localScale;
        TempScale.x = scale; TempScale.z = scale;
        GetComponentInChildren<CapsuleCollider>().transform.localScale = TempScale;
    }
    void Update() {
        float deltaTime = Time.fixedDeltaTime;
        if(start){
            if(OnGround){
                transform.Translate(new Vector3(0f, 0f, speed * deltaTime));
                if(Random.Range(0f, deltaTime) >= deltaTime * 0.98f){
                    transform.Rotate(0f, Random.Range(-40f, 40f), 0f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ground")){
            if(!OnGround){
                StartCoroutine(OnGroundCoroutine(1f));
            }
        }
        if(other.CompareTag("Friendly")){
            animalControl.PlaySoundDeath();
            other.gameObject.SetActive(false);
            animalControl.Animals.Remove(other.gameObject);
            animalControl.ChangeScore(false);
            animalControl.CheckEnd();
        }else if(!other.CompareTag("ClickEffect")){
            transform.LookAt(other.transform.position);
            Quaternion rotation = transform.rotation;
            rotation.x = 0f; rotation.z = 0f;
            transform.rotation = rotation;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    IEnumerator OnGroundCoroutine(float time){
        yield return new WaitForSeconds(time);
        OnGround = true;
    }
    #endregion
}
