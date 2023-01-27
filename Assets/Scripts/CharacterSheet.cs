using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSheet : MonoBehaviour
{
    
    public IDictionary<TMP_Text,int> Stats = new Dictionary<TMP_Text, int>();
    [SerializeField] private TMP_Text[] statText;
    [SerializeField] int baseStat = 5;
    [SerializeField] int maxStat = 10;
    [SerializeField] int minStat = 1;
    [SerializeField] int maxTotal = 40; 
    // Start is called before the first frame update
    void Start()
    {
        int statLength = statText.Length;
        for(int i = 0; i < statLength;i++)
        {
            statText[i].SetText(baseStat.ToString());
            Stats.Add(statText[i],baseStat);
            Debug.Log(Stats.Values.Sum());
        }
    }
    public void posIncrement(TMP_Text posText)
    {
        if(Stats.ContainsKey(posText) && Stats.Values.Sum() < maxTotal && Stats[posText] < maxStat)
        {
            Stats[posText]++;
            posText.SetText(Stats[posText].ToString());
            Debug.Log(Stats.Values.Sum());
        }
    }
    public void negIncrement(TMP_Text negText)
    {
        if(Stats.ContainsKey(negText) && Stats[negText] > minStat)
        {
            Stats[negText]--;
            negText.SetText(Stats[negText].ToString());
            Debug.Log(Stats.Values.Sum());
        }
    }
    //AAAAAAAAAAAAAAH 
}
