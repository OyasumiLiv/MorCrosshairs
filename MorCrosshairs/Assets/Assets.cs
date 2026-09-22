using UnityEngine;
using System.IO;
using System.Reflection;
using BepInEx.Logging;

namespace MorCrosshairs.Assets
{
    public class BuildAssets
    {
        public static Sprite GetIcon()
        {
            string iconPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "icon.png");

            if (!File.Exists(iconPath))
            {
                Main.logger.LogInfo("Icon not found.");
                return null;
            }
            
            Texture2D icon = new Texture2D(256, 256, TextureFormat.ARGB32, false);
            
            ImageConversion.LoadImage(icon, File.ReadAllBytes(iconPath));
            
            return Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector2.zero);
        }
    }
}
