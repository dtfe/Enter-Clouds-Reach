using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public Dictionary<string,int> Stats = new Dictionary<string, int>();
   //public IDictionary<string,PlayerTraits> Traits = new Dictionary<string,PlayerTraits>();
    public PlayerTraits[] traitArray;
    public PlayerTraits scared = new PlayerTraits("Scared",-1,0,0,-1,1,0,false);
    public PlayerTraits brave = new PlayerTraits("Brave",1,0,0,0,-1,1,false);
    
    public T[] GetVariablesOfType<T>()
    {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        T[] variables = new T[fields.Length];
        int count = 0;
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(T))
            {
                variables[count] = (T)field.GetValue(this);
                count++;
            }
        }
        System.Array.Resize(ref variables, count);
        return variables;
    }
}