using IsaacMod.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static IsaacMod.IsaacMod;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;

namespace IsaacMod.Content.Items.Collectibles
{
	public class Suplex : Collectible
	{
        public override bool isPassive => false;
        public static string Id { get { return "Suplex"; } }
        public override string ID => Id;
        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                Vector2 vel = (Main.MouseWorld - player.Center).SafeNormalize(new Vector2(1, 0)) * 30;
                player.Isaac().suplexDashTime = 30;
                player.Isaac().suplexDashvel = vel;
                
                if(Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket p = Mod.GetPacket();
                    p.Write(NetPackets.SUPLEX);
                    p.Write(player.whoAmI);
                    p.Write(30);
                    p.WriteVector2(vel);
                    p.Send();
                }
            }
            return true;
        }
        public override float getWeight()
        {
            return 0.2f;
        }
    }
}
