using IsaacMod.Content.Rarities;
using IsaacMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Accessories
{
	public abstract class Collectible : ModItem
	{
        public override void SetDefaults() {
			Item.width = 64;
			Item.height = 64;
            Item.value = 2000;
            Item.rare = ModContent.RarityType<Collectibles>();
            Item.accessory = true;
			
		}
    }
}
