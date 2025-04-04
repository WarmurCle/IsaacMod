using IsaacMod.Content.Items.Collectibles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace IsaacMod.Common
{
    public class BossDownedRecordSys : ModSystem
    {
        public static List<int> downedBosses = new List<int>();
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(downedBosses.Count);
            for (int i = 0; i < downedBosses.Count; i++)
            {
                writer.Write(downedBosses[i]);
            }
        }
        public override void NetReceive(BinaryReader reader)
        {
            downedBosses.Clear();
            int count = reader.ReadInt32();
            for(int i = 0; i < count; i++)
            {
                downedBosses.Add(reader.ReadInt32());
            }
        }
        public override void SaveWorldData(TagCompound tag)
        {
            if (downedBosses.Count > 0) {
                tag.Add("BossRecordCount", downedBosses.Count);
                for (int i = 0; i < downedBosses.Count; i++)
                {
                    tag.Add("r" + i.ToString(), downedBosses[i]);
                }
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("BossRecordCount"))
            {
                downedBosses.Clear();
                int c = tag.GetAsInt("BossRecordCount");
                for(int i = 0; i < c; i++)
                {
                    downedBosses.Add(tag.GetAsInt("r" + i.ToString()));
                }
            }
        }

        public override void ClearWorld()
        {
            downedBosses.Clear();
        }
    }

    public class BossDownedRecordGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void OnKill(NPC npc)
        {
            if (npc.boss && npc.realLife < 0)
            {
                if (!BossDownedRecordSys.downedBosses.Contains(npc.whoAmI))
                {
                    BossDownedRecordSys.downedBosses.Add(npc.whoAmI);
                    Collectible.spawnNewItemFromItemPool(ItemPools.NormalDrop, npc.GetSource_Death(), npc.getRect());
                }
            }
        }
    }
}
