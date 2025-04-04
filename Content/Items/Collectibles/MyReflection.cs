using IsaacMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Collectibles
{
	public class MyReflection : Collectible
	{
        public static string Id { get { return "MyReflection"; } }
        public override string ID => Id;
        public override float getWeight()
        {
            return 4;
        }

    }
}
