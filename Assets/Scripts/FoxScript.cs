using UnityEngine;
using System.Collections;
public class FoxScript : MonoBehaviour
{
    
    #region Variables
    public float scale = 5f;
    public bool AdvancedMove; public bool MoveAdvanced; public int MoveRounds;
    AnimalControl animalControl; Vector3 originalPos; public float speed = 5f;
    Animator animator; public FoxControllerL9 foxController; bool origin = true;
    public float timeToNext; bool OnGround = false; public bool start;
    #endregion

    #region Unity methods

    void Start()
    {
        if(AdvancedMove){
            originalPos = transform.position;
        }
        if(foxController){
            foxController.Foxes.Add(gameObject);
        }
        MoveRounds = 2;
        // if(MoveRounds % 2 != 0){
        //     MoveRounds++;
        // }
        animator = GetComponent<Animator>();
        animalControl = GameObject.Find("AnimalControl").GetComponent<AnimalControl>();
        animalControl.HarmAnimalsFox.Add(gameObject);
        Vector3 TempScale = GetComponentInChildren<CapsuleCollider>().transform.localScale;
        TempScale.x = scale; TempScale.z = scale;
        GetComponentInChildren<CapsuleCollider>().transform.localScale = TempScale;
    }
    void Update() {
        if(start){
            if(OnGround){
                if(MoveAdvanced){
                    transform.Translate(0f, 0f, speed * Time.fixedDeltaTime);
                    // if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")){
                    if(Time.time - timeToNext >= 0.5f){
                        transform.Rotate(0f, 180f, 0f);
                        animator.Play("Jump");
                        timeToNext = Time.time;
                        MoveRounds--;
                    }
                    if(MoveRounds <= 0){
                        transform.position = originalPos;
                        MoveAdvanced = false;
                        foxController.BackToPlace = true;
                        MoveRounds = 2;
                        // transform.Rotate(0f, 180f, 0f);
                        // timeToNext = Time.time;
                        // transform.position = originalPos;
                        // if(MoveRounds % 2 != 0){
                        //     MoveRounds++;   
                        // }
                    }
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
        }
    }
    IEnumerator OnGroundCoroutine(float time){
        yield return new WaitForSeconds(time);
        originalPos = transform.position;
        OnGround = true;
    }
    #endregion
}
