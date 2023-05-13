using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    private int goldleaf;
    private int goldleafCorrupted;

    [SerializeField] private TextMeshProUGUI goldleafText;
    [SerializeField] private TextMeshProUGUI goldleafCorruptedText;

    public void Goldleaf(bool isCorrupted)
    {
        if (isCorrupted)
        {
            goldleafCorrupted++;
        }
        else
        {
            goldleaf++;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        goldleafCorruptedText.text = goldleafCorrupted.ToString();
        goldleafText.text = goldleaf.ToString();
    }

    public void ConvertCorruptedGoldleaf()
    {
        if (goldleafCorrupted > 0)
        {
            goldleafCorrupted--;
            goldleaf++;
            Debug.Log("Converted goldleaf");
        }
        else
        {
            Debug.Log("No corrupted goldleaf!");
        }
        UpdateUI();
    }
}
