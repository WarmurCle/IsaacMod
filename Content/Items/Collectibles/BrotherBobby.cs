using IsaacMod.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Collectibles
{
	public class BrotherBobby : Collectible
	{
        public static string Id { get { return "BrotherBobby"; } }
        public override string ID => Id;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
    }
}
