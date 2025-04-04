
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace IsaacMod.Common
{
    public class IsaacGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int suplexTime = 0;
        public int suplexPlayer = 0;

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (suplexTime > 0)
            {
                Texture2D tex = TextureAssets.Npc[npc.type].Value;
                Rectangle frame = npc.frame;
                float rot = 0;
                Vector2 offset = new Vector2(0, -40);
                if(suplexTime <= 30)
                {
                    rot = MathHelper.Pi;
                    offset *= -1;
                }
                Vector2 posdraw = Main.player[suplexPlayer].Center + offset - Main.screenPosition;
                Main.spriteBatch.Draw(tex, posdraw, frame, drawColor, rot, new Vector2(tex.Width / 2, (float)tex.Height / (float)Main.npcFrameCount[npc.type]), npc.scale * new Vector2(0.4f, 1f / 0.4f), SpriteEffects.None, 0);
                return false;
            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
}
