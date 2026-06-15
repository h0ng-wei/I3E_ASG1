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
            Collectibles col = other.GetComponentInParent<Collectibles>();
            if (col != null)
            {
                collectables++;
                points += col.points;

                Debug.Log("Points value on this collectible: " + col.points);
                Debug.Log("Collectables: " + collectables);
                Debug.Log("Points: " + points);

                col.Collect();

                SetCollectablesText();
                SetPointsText();
            }
        }
    }

}
