using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private Image blockColor;
    private TextMeshProUGUI blockText;
    private TextMeshProUGUI blockTextColor;

    void Start()
    {
        blockColor = gameObject.GetComponent<Image>();

        blockText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        blockTextColor = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        switch(blockText.text) // ADAPT THE BACKGROUND COLOR OF THE BLOCK
        {
            case "":
                blockColor.color = new Color32(0x34, 0x3A, 0x4C, 0xFF);
                break;

            case "2":
                blockColor.color = new Color32(0x3B, 0x73, 0xEB, 0xFF);
                break;

            case "4":
                blockColor.color = new Color32(0xFF, 0xE5, 0x59, 0xFF);
                break;

            case "8":
                blockColor.color = new Color32(0xDF, 0x75, 0x45, 0xFF);
                break;

            case "16":
                blockColor.color = new Color32(0x7F, 0x40, 0xC2, 0xFF);
                break;

            case "32":
                blockColor.color = new Color32(0x69, 0xCA, 0x69, 0xFF);
                break;

            case "64":
                blockColor.color = new Color32(0xA9, 0xDA, 0x4F, 0xFF);
                break;

            case "128":
                blockColor.color = new Color32(0x39, 0x5F, 0xCD, 0xFF);
                break;

            case "256":
                blockColor.color = new Color32(0x39, 0xB3, 0xCD, 0xFF);
                break;

            case "512":
                blockColor.color = new Color32(0xC1, 0x63, 0xDE, 0xFF);
                break;

            case "1024":
                blockColor.color = new Color32(0xFF, 0x6B, 0x90, 0xFF);
                break;

            case "2048":
                blockColor.color = new Color32(0xFF, 0x6B, 0x5B, 0xFF);
                break;
        }

        switch(blockText.text.Length) // ADAPT FONT SIZE
        {
            case 1:
                blockText.fontSize = 90;
                break;

            case 2:
                blockText.fontSize = 78;
                break;

            case 3:
                blockText.fontSize = 65;
                break;

            case 4:
                blockText.fontSize = 50;
                break;
        }
    }
}
