using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    int collectables;

    [SerializeField]
    int points;

    [SerializeField]
    TextMeshProUGUI collectablesText;

    [SerializeField]
    TextMeshProUGUI pointsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectables = 0;
        points = 0;

        SetCollectablesText();
        SetPointsText();
    }

    // Update is called once per frame
    void SetCollectablesText()
    {
        collectablesText.text = "Collectables: " + collectables.ToString() + " / 40";
    }

    void SetPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectibles"))
        {
            collectables++;
            points++;

            Collectibles col = other.GetComponent<Collectibles>();
            if (col != null)
            col.Collect();

            Debug.Log("Collectables: " + collectables);
            Debug.Log("Points: " + points);

            SetCollectablesText();
            SetPointsText();
        }
    }

}
