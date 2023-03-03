using UnityEngine;

[CreateAssetMenu(menuName = "Trait")]
public class Trait : ScriptableObject
{
    public PlayerTraits playerTraits;
    public string TraitName;
    public string TraitDescription;
    private bool defaultBool = false;

    private void OnEnable()
    {
        playerTraits = new PlayerTraits(TraitName,TraitDescription,defaultBool);
    }
}