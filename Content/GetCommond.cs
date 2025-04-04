using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using IsaacMod.Utils;
using Microsoft.Xna.Framework.Input;
using IsaacMod.Common;

namespace IsaacMod.Content
{
    public class GetCommond : ModCommand
    {
        public static bool enabled = true;
        public override CommandType Type
            => CommandType.Chat;

        // The desired text to trigger this command
        public override string Command
            => "Isaac";

        // A short usage explanation for this command
        public override string Usage
            => "/Isaac" +
            "\n get\n reset\n add";

        // A short description of this command
        public override string Description
            => "Isaac Mod Debug";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (enabled)
            {
                if (args.Length > 0)
                {
                    if (args[0].Equals("get"))
                    {
                        Main.NewText("Collectibles" + ":", Color.Yellow);
                        foreach(string key in Main.LocalPlayer.Isaac().collectiblesOwned.Keys) {
                            Main.NewText(key + ":" + Main.LocalPlayer.Isaac().getCollectibleCount(key), Color.Yellow);
                        }
                    }
                    if (args[0].Equals("set"))
                    {
                        Main.LocalPlayer.Isaac().collectiblesOwned[args[1]] = int.Parse(args[2]);
                        Main.NewText("Set " + args[1] + " To " + args[2], Color.Yellow);
                    }
                    if (args[0].Equals("add"))
                    {
                        foreach (string i in IsaacMod.CollectiblesID)
                        {
                            if (i.ToLower().Equals(args[1]))
                            {
                                Main.LocalPlayer.Isaac().AddCollectible(i, true, int.Parse(args[2]));
                                Main.NewText("Add " + args[2] + " " + i, Color.Yellow);
                                break;
                            }
                        }
                    }
                    if (args[0].Equals("reset"))
                    {
                        BossDownedRecordSys.downedBosses.Clear();
                        Main.NewText("Reseted boss killed count", Color.Yellow);
                    }
                    if (args[0].Equals("all"))
                    {
                        Main.NewText("All", Color.Yellow);
                        foreach (string s in IsaacMod.CollectiblesID)
                        {
                            Main.NewText(s, Color.Yellow);
                        }
                    }
                    if (args[0].Equals("clear"))
                    {
                        Main.LocalPlayer.Isaac().collectiblesOwned.Clear();
                        Main.NewText("Cleared your collectibles", Color.Yellow);
                    }
                    if (args[0].Equals("boss"))
                    {
                        Main.NewText(BossDownedRecordSys.downedBosses.GetItems_String(), Color.Yellow);
                    }
                }
                else
                {
                    Main.NewText("No args", Color.Red);
                }
            }
            else { Main.NewText("Isaac Mod Debug is DISABLED", Color.Red); }
        }
    }
}
