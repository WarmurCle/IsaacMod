using IsaacMod.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace IsaacMod.Utils
{
    public static class Util
    {
        public static string GetItems_String(this List<int> l)
        {
            string s = "";
            for(int i = 0; i < l.Count; i++)
            {
                s += l[i].ToString();
                if(i != l.Count - 1)
                    s += ", ";
            }
            return s;
        }
        public static IsaacPlayer Isaac(this Player player)
        {
            return player.GetModPlayer<IsaacPlayer>();
        }

        public static IsaacGlobalItems Isaac(this Item item)
        {
            return item.GetGlobalItem<IsaacGlobalItems>();
        }

        public static IsaacGlobalNPC Isaac(this NPC npc)
        {
            return npc.GetGlobalNPC<IsaacGlobalNPC>();
        }
        public static IsaacGlobalProjectile Isaac(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<IsaacGlobalProjectile>();
        }
    }
}
