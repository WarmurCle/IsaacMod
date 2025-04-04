using IsaacMod.Common;
using IsaacMod.Content.Items.Collectibles;
using IsaacMod.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace IsaacMod
{
	public class IsaacMod : Mod
	{
        public static List<string> CollectiblesID;
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                IsaacGlobalProjectile.heldProjs.Add(CalamityWeakRef.ItemType.hGaleProj);

            }
        }
        public static class NetPackets
        {
            public static byte SUPLEX = 0;
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte type = reader.ReadByte();
            if (type == NetPackets.SUPLEX)
            {
                int player = reader.ReadInt32();
                int time = reader.ReadInt32();
                Vector2 vel = reader.ReadVector2();
                Main.player[player].velocity = vel;
                if (Main.dedServ)
                {
                    ModPacket p = this.GetPacket();
                    p.Write(NetPackets.SUPLEX);
                    p.Write(player);
                    p.Write(time);
                    p.WriteVector2(vel);
                    p.Send();
                }
            }
        }
        public override void Load()
        {
            On_NPC.UpdateNPC += updatenpc;
            ItemPools.SetUpPools();
        }

        private void updatenpc(On_NPC.orig_UpdateNPC orig, NPC self, int i)
        {
            if (self.active && self.Isaac().suplexTime > 0)
            {
                self.Isaac().suplexTime--;
            }
            else
            {
                orig(self, i);
            }
        }


        public override void Unload()
        {
            CollectiblesID = null;
            On_NPC.UpdateNPC -= updatenpc;
            ItemPools.Clear();
            Collectible.InitList = null;
        }
        public override object Call(params object[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] is string str)
                {
                    if (str.Equals("HeldProj"))
                    {
                        IsaacGlobalProjectile.heldProjs.Add((int)args[1]);
                    }
                    else if (str.Equals("SetInnerEyeSpawn"))
                    {
                        IsaacGlobalProjectile.canInnerEyeSpawnProjNow = (bool)args[1];
                    }
                    else if (str.Equals("GetInnerEyeSpawn"))
                    {
                        return IsaacGlobalProjectile.canInnerEyeSpawnProjNow;
                    }
                }
            }
            return null;
        }

    }
}
