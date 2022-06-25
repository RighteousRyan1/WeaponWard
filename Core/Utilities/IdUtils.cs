using System;
using ReLogic.Reflection;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponWard.Core.Utilities
{
    public static class IdUtils
    {
        public static int? ToItemId(this Identifier id) {
            return ToNumericalId<ModItem>(id, ItemID.Search, content => content.Type);
        }

        private static int? ToNumericalId<TContent>(Identifier id, IdDictionary dict, Func<TContent, int> getId)
            where TContent : IModType {
            if (id.Namespace == "Terraria") {
                if (dict.ContainsName(id.Content)) return dict.GetId(id.Content);
                return null;
            }

            if (!ModLoader.TryGetMod(id.Namespace, out Mod? mod)) return null;

            if (mod.TryFind(id.Content, out TContent content)) {
                return getId(content);
            }

            return null;
        }
    }
}