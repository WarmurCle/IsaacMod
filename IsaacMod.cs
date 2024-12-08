using IsaacMod.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace IsaacMod
{
	public class IsaacMod : Mod
	{
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                IsaacGlobalProjectile.heldProjs.Add(CalamityWeakRef.ItemType.hGaleProj);

            }
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
