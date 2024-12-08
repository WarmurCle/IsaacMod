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
        public static IsaacPlayer Isaac(this Player player)
        {
            return player.GetModPlayer<IsaacPlayer>();
        }

        public static IsaacGlobalItems Isaac(this Item item)
        {
            return item.GetGlobalItem<IsaacGlobalItems>();
        }

        public static IsaacGlobalProjectile Isaac(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<IsaacGlobalProjectile>();
        }
    }
}
