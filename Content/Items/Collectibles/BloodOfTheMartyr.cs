using IsaacMod.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Collectibles
{
	public class BloodOfTheMartyr : Collectible
	{
        public static string Id { get { return "BloodOfTheMartyr"; } }
        public override string ID => Id;
        public override float getWeight()
        {
            return 4;
        }
    }
}
