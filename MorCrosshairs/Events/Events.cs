using RoR2;
using RoR2.UI;

using UnityEngine;

namespace MorCrosshairs.Events
{
    public interface IEvents
    {
        void CrosshairManager_UpdateCrosshair(On.RoR2.UI.CrosshairManager.orig_UpdateCrosshair orig, CrosshairManager self, CharacterBody targetBody, Vector3 crosshairWorldPosition, Camera uiCamera, Color crosshairColor, float crosshairScale);
    }
}
