using UnityEngine;
using TMPro;

public class TreeCutting : MonoBehaviour
{
    public float cuttingTimeThreshold = 2f; 
    public float woodMinAmount = 5f; 
    public float woodMaxAmount = 10f;
    public TextMeshProUGUI woodAmountText;

    private bool isCutting = false;
    private float currentCuttingTime = 0f;
    private int trees = 0;
    private float currentWoodAmount = 0f;
    int currentWood = 0;
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
               
                if (hit.collider.CompareTag("Tree"))
                {
                    StartCutting();
                }
            }
        }

      
        if (isCutting)
        {
            currentCuttingTime += Time.deltaTime;

      
            if (currentCuttingTime >= cuttingTimeThreshold)
            {
             
                CutTree();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }
    }

    void StartCutting()
    {
        isCutting = true;
    }

    void StopCutting()
    {
        isCutting = false;
        currentCuttingTime = 0f;
    }

    void CutTree()
    {
        StopCutting();

        
        Destroy(gameObject);

        
         currentWoodAmount = Random.Range(woodMinAmount, woodMaxAmount);

        currentWood = (int)currentWoodAmount;

       UpdateWoodAmountText();
      
    }
    void UpdateWoodAmountText()
    {

        if (currentWood != null)
        {
            woodAmountText.text = ": " + currentWood.ToString();
        }
    }
}
