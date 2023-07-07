using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameSettings : MonoBehaviour
{
    [SerializeField] int minNumberOfStages = 1;
    [SerializeField] int maxNumberOfStages = 18;
    int holesSet;
    [SerializeField] TMP_InputField holesInputField;

    public void CheckNumberOfStages(){
        string test = holesInputField.text;
        
        holesSet = int.Parse(test);

        if (holesSet > maxNumberOfStages){
            holesSet = maxNumberOfStages;
        } else if (holesSet < minNumberOfStages){
            holesSet = minNumberOfStages;
        }
        holesInputField.text = holesSet.ToString();
    }
}
