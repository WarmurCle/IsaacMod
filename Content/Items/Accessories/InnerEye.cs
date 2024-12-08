using IsaacMod.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Accessories
{
	public class InnerEye : Collectible
	{
        public static string Id { get { return "InnerEye"; } }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Isaac().setCollectible(Id, true);
        }

        public static List<float> getRots(Player player)
        {
            return new List<float>() { 0.2f, -0.2f };
        }
        
    }
}
