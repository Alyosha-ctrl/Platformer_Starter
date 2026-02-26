using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class deathcube : MonoBehaviour
{

    public TextMeshProUGUI centerText;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("LOST!");
        TextMeshProUGUI center = GameObject.Find("Canvas/Center").GetComponent<TextMeshProUGUI>();
        center.text = "YOU LOST!";
        // centerText.text = "YOU LOSE!";
        Destroy(other.gameObject);
    }
}
