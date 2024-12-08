using IsaacMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Accessories
{
	public class TheSadOnion : Collectible
	{
        public static string Id { get { return "SadOnion"; } }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Isaac().setCollectible(Id, true);
            player.GetAttackSpeed(DamageClass.Generic) += 0.7f;
        }

        
    }
}
