using IsaacMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Accessories
{
	public class SpoonBender : Collectible
	{
        public static string Id { get { return "SpoonBender"; } }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Isaac().setCollectible(Id, true);
        }

        
    }
}
