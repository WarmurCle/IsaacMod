using IsaacMod.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Collectibles
{
	public class InnerEye : Collectible
	{
        public static string Id { get { return "InnerEye"; } }
        public override string ID => Id;
        public override float getWeight()
        {
            return 1;
        }
        public static List<float> getRots(Player player)
        {
           List<float> rots = new List<float>();
            for(int i = 1; i <= player.Isaac().getCollectibleCount(InnerEye.Id); i++)
            {
                rots.Add(i * 0.2f);
                rots.Add(i * -0.2f);

            }
            return rots;
        }
        
    }
}
