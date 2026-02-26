using BepInEx;
using RoR2;

namespace LunarsAreForever
{
    // Metadata
    [BepInPlugin("Samuel17.LunarsAreForever", "LunarsAreForever", "1.0.0")]

    public class Main : BaseUnityPlugin
    {
        public static ItemTier highlanderTier;

        public void Awake()
        {
            // Logging!
            Log.Init(Logger);

            // Mod logic
            ItemTierCatalog.availability.CallWhenAvailable(HighlanderTierFix);
            ItemCatalog.availability.CallWhenAvailable(NoTemporaryLunars);
        }

        private void HighlanderTierFix()
        {
            ItemTierDef highlanderTierDef = ItemTierCatalog.FindTierDef("Highlander");
            if (highlanderTierDef)
            {
                highlanderTier = highlanderTierDef.tier;
                On.RoR2.Util.RollTemporaryItemFromItemIndex += RollTemporaryItemFromItemIndex;
            }
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

        private ItemIndex RollTemporaryItemFromItemIndex(On.RoR2.Util.orig_RollTemporaryItemFromItemIndex orig, ItemIndex itemIndex)
        {
            ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
            if (itemDef && itemDef.tier == highlanderTier)
            {
                return ItemIndex.None;
            }
            
            return orig(itemIndex);
        }
    }
}
