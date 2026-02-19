using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class script : MonoBehaviour
{
    //Get a bunch of public vars for the text mesh
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    public Transform mainCamera;

    //Then a bunch of child private vars for each. Each update move the apropriate ones around
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float timerTime = 400f;
    int score = 0;
    int coins = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input handler for Part1
        if(Keyboard.current.leftArrowKey.isPressed) 
            mainCamera.position = new Vector3(mainCamera.position.x - 1, mainCamera.position.y, mainCamera.position.z);
        if(Keyboard.current.rightArrowKey.isPressed) 
            mainCamera.position = new Vector3(mainCamera.position.x + 1, mainCamera.position.y, mainCamera.position.z);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //Get the position of the mouse
            Vector2 screenPosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            //Create a ray
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray.origin, ray.direction, out rayHit)){
                if(rayHit.transform.tag == "brick")
                {
                    Destroy(rayHit.transform.gameObject);
                    addScore(1);
                }
                else if(rayHit.transform.tag == "question")
                {
                    addCoin();
                }
            }
            
            //Get a rayhit

            //If it hits destroy that object if the tag is brick
            //Don't destroy and add one coin if the tag is question.
        }
        //Change time.
        timerTime -= Time.deltaTime;

        //Set text.
        setText();
    }

    void setText()
    {
        timerText.text = $"Time\n{timerTime.ToString("F0")}";
        scoreText.text = $"Score\n{score}";
        coinText.text = $"Coins\n{coins}";
    }

    public void addCoin()
    {
        coins+=1;
    }

    public void addScore(int scoreAmount)
    {
        score+=scoreAmount;
    }
}