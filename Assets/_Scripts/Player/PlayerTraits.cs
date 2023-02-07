using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerTraits
{
    public string traitName;
    public int brawnModifier;
    public int agilityModifier;
    public int enduranceModifier;
    public int knowledgeModifier;
    public int wisdomModifier;
    public int charmModifier;
    public int totalMod;

    public PlayerTraits(string traitName, int brawnModifier, int agilityModifier, int enduranceModifier, int knowledgeModifier, int wisdomModifier, int charmModifier)
    {   
        totalMod = brawnModifier+agilityModifier+enduranceModifier+knowledgeModifier+wisdomModifier+charmModifier;
        this.traitName = traitName;
        this.brawnModifier = brawnModifier;
        this.agilityModifier = agilityModifier;
        this.enduranceModifier = enduranceModifier;
        this.knowledgeModifier = knowledgeModifier;
        this.wisdomModifier = wisdomModifier;
        this.charmModifier = charmModifier;
    }
}