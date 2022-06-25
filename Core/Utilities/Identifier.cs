using System;

namespace WeaponWard.Core.Utilities
{
    /// <summary>
    ///     Represents a string lexer identifier.
    /// </summary>
    /// <param name="Namespace">The namespace this content belongs to.</param>
    /// <param name="Content">The content name.</param>
    public readonly record struct Identifier(string Namespace, string Content)
    {
        public override string ToString() {
            return $"{Namespace}:{Content}";
        }

        public static Identifier Parse(string value) {
            if (TryParse(value, out Identifier id)) return id;
            throw new ArgumentException($"Invalid identifier: {value}");
        }

        public static bool TryParse(string value, out Identifier id) {
            string[] split = value.Split(':', 2);

            if (split.Length != 2) {
                id = new Identifier();
                return false;
            }

            id = new Identifier(split[0], split[1]);
            return true;
        }

        public static implicit operator string(Identifier id) {
            return id.ToString();
        }

        public static implicit operator Identifier(string id) {
            return Parse(id);
        }
    }
}