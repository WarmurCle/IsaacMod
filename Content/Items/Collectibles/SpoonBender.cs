using IsaacMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Collectibles
{
	public class SpoonBender : Collectible
	{
        public static string Id { get { return "SpoonBender"; } }
        public override string ID => Id;
        public override float getWeight()
        {
            return 0.9f;
        }
    }
}
