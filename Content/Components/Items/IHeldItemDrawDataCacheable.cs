using Terraria.DataStructures;

namespace WeaponWard.Content.Components.Items
{
    /// <summary>
    ///     Allows you to manipulate an item's draw data handling for HeldItem logic.
    /// </summary>
    public interface IHeldItemDrawDataCacheable
    {
        /// <summary>
        ///     Allows you to manipulate draw data and cache data to draw before vanilla caching. Return value manipulates how registration happens.
        /// </summary>
        /// <param name="drawInfo">The draw set.</param>
        /// <param name="drawData">The draw data.</param>
        /// <returns><see langkey="true"/> to add the draw data to the draw set, <see langkey="false"/> to not.</returns>
        bool PreCacheDrawData(ref PlayerDrawSet drawInfo, ref DrawData drawData);

        /// <summary>
        ///     Allows you to cache data to draw after vanilla caching. Ran after <see cref="PreCacheDrawData"/>.
        /// </summary>
        /// <param name="drawInfo"></param>
        /// <param name="drawData"></param>
        void PostCacheDrawData(PlayerDrawSet drawInfo, DrawData drawData);
    }
}