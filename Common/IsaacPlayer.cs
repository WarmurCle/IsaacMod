using IsaacMod.Content.Items.Accessories;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

public class IsaacPlayer : ModPlayer
{
    public Dictionary<string, bool> collectiblesEnabled = new Dictionary<string, bool>();
    public void setCollectible(string ID, bool value)
    {
        collectiblesEnabled[ID] = value;
    }
    public bool hasCollectible(string ID)
    {
        if (!collectiblesEnabled.ContainsKey(ID))
        {
            return false;
        }
        return collectiblesEnabled[ID];
    }

    public override void ResetEffects()
    {
        foreach (string key in collectiblesEnabled.Keys)
        {
            collectiblesEnabled[key] = false;
        }
    }

    public override void PostUpdate()
    {
    }

}