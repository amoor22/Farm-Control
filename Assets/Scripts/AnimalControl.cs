using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AnimalControl : MonoBehaviour
{
    
    #region Variables
    public List<GameObject> Animals = new List<GameObject>();
    public List<GameObject> AnimalTypes = new List<GameObject>(); 
    public List<GameObject> HarmAnimalsFox = new List<GameObject>();
    public List<GameObject> HarmAnimalsDog = new List<GameObject>();
    public List<GameObject> Birds = new List<GameObject>();
    public int AnimalCount;
    public GameObject SpawnGround;
    public TextMeshProUGUI AliveText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PlayButton;
    public FarmerLoader farmerLoader; 
    private int Alive = 0; public AudioSource DeathSound; public AudioSource AliveSound;
    int Score = 0; bool finished = false; bool restart; int SceneStartScore; public int CurrentScore;
    public AudioSource LevelFinishSound; int count;

    #endregion

    #region Unity methods

    void Start()
    {
        Score = PlayerPrefs.GetInt("score", 0);
        CurrentScore = PlayerPrefs.GetInt("score");
        SceneStartScore = Score;
        for (int i = 0; i < AnimalCount; i++){
            Spawn(AnimalTypes[Random.Range(0, AnimalTypes.Count)]);
        }
        ScoreText.text = string.Format("Score : {0}", Score);
    }

    public void ChangeAlive(){
        Alive++;
        AliveText.text = Alive.ToString();
    }
    public void ChangeScore(bool plus){
        if(plus){
            CurrentScore += 5;
        }else{
            CurrentScore -= 5;
        }
        ScoreText.text = string.Format("Score : {0}", CurrentScore);
    }
    public void UpdateScore(){
        ScoreText.text = string.Format("Score : {0}", CurrentScore);
    }
    public void PurchaseFarmer(){
        CurrentScore -= 50;
        UpdateScore();
    }
    public void StartMove(){
        foreach(GameObject anim in Animals){
            anim.GetComponent<SheepMovement>().play = true;
        }
        foreach(GameObject bird in Birds){
            bird.GetComponent<BirdScript>().play = true;
        }
        foreach(GameObject dog in HarmAnimalsDog){
            dog.GetComponent<DogMovement>().start = true;
        }
        foreach(GameObject fox in HarmAnimalsFox){
            fox.GetComponent<FoxScript>().start = true;
        }
        if(farmerLoader != null){
            farmerLoader.start = true;
        }
    }
    public void PlaySoundDeath(){
        DeathSound.Play();
    }
    public void PlaySoundAlive(){
        AliveSound.Play();
    }
    public void PlaySoundWin(){
        LevelFinishSound.Play();
    }
    public void CheckEnd(){
        count = Animals.Count + Birds.Count;
        if(Alive == AnimalCount){
            //end - win - (next)
            PlayerPrefs.SetInt("score", CurrentScore);
            if(PlayerPrefs.GetInt("lastLevel") < SceneManager.sceneCountInBuildSettings - 1){
                PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex + 1);
            }else{
                PlayerPrefs.SetInt("lastLevel", 0);
            }
            PlaySoundWin();
            PlayButton.text = "Well Done! Next Level";
            finished = true;
        }
        else if(count == 0 && Alive != AnimalCount){
            // end - lose - (restart)
            PlayButton.text = "Restart Level";
            restart = true;
        }
    }
    void Spawn(GameObject animal){
        MeshRenderer SpawnMesh = SpawnGround.GetComponent<MeshRenderer>();
        Vector3 position = new Vector3(Random.Range(SpawnGround.transform.position.x - SpawnMesh.bounds.size.x / 2f, SpawnGround.transform.position.x + SpawnMesh.bounds.size.x / 2f), Random.Range(1f, 3f), Random.Range(SpawnGround.transform.position.z - SpawnMesh.bounds.size.z / 2f, SpawnGround.transform.position.z + SpawnMesh.bounds.size.z / 2f));
        Instantiate(animal, position, transform.rotation = Quaternion.Euler(0f, Random.Range(-180f, 180f), 0f));
    }
    public void NextLevel(){
        if(finished){
            SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));
        }
        if(restart){
            PlayerPrefs.SetInt("score", SceneStartScore);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("score", SceneStartScore);
    }
    public void Menu(){
        
        SceneManager.LoadScene(0);
    }
    #endregion
}
