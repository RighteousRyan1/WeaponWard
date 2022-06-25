namespace WeaponWard.Content.Components.Items
{
    /// <summary>
    ///     Represents what inspiration this item takes from the base game.
    /// </summary>
    public enum WardItemType
    {
        /// <summary>
        ///     Completely original content.
        /// </summary>
        Original,

        /// <summary>
        ///     Content taken directly from the game.
        /// </summary>
        Unoriginal,

        /// <summary>
        ///     Content taken from the game, but heavily modified.
        /// </summary>
        Adapted,
    }

    /// <summary>
    ///     Core component for Weapon Ward items.
    /// </summary>
    public interface IWardItem
    {
        /// <summary>
        ///     This content's item originality type.
        /// </summary>
        WardItemType ItemType { get; }

        string ItemAsylumWikiLink { get; }
    }
}