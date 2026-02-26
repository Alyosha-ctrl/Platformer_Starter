using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class vCube : MonoBehaviour
{

    public TextMeshProUGUI centerText;
    public TextMeshProUGUI time;

    // void OnCollisionEnter(Collision collision)
    // {
    //     centerText.text = "YOU WIN!";
    //     Debug.Log("WON!");
    // }

    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     centerText.text = "YOU WIN!";
    //     Debug.Log("WON!");
    // }

    void OnTriggerEnter(Collider other)
    {
        TextMeshProUGUI center = GameObject.Find("Canvas/Center").GetComponent<TextMeshProUGUI>();
        center.text = "YOU WON!";
        Debug.Log("WON!");
    }
}
