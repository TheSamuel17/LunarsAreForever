using BepInEx;
using RoR2;

namespace LunarsAreForever
{
    // Metadata
    [BepInPlugin("Samuel17.LunarsAreForever", "LunarsAreForever", "1.0.1")]

    public class Main : BaseUnityPlugin
    {
        public void Awake()
        {
            // Logging!
            Log.Init(Logger);

            // Mod logic
            ItemCatalog.availability.CallWhenAvailable(NoTemporaryLunars);
        }

        private void NoTemporaryLunars()
        {
            foreach (ItemDef itemDef in ItemCatalog.allItemDefs)
            {
                if (itemDef.tier == ItemTier.Lunar && itemDef.ContainsTag(ItemTag.CanBeTemporary))
                {
                    itemDef.tags = itemDef.tags.RemoveFromArray(ItemTag.CanBeTemporary);
                    Log.Message("Removed CanBeTemporary tag from " + itemDef.nameToken + ".");
                }
            }
        }
    }
}
