
namespace lan_scanner.gui.other
{
    public static class ResourceCache
    {
        private static readonly Dictionary<string, Font> fontCache = new();
        private static readonly Dictionary<string, Label> labelCache = new();
        private static readonly Dictionary<string, Pen> penCache = new();

        // =====================================================================================================
        public static Font GetFont(string fontFamily, float fontSize, FontStyle fontStyle)
        {
            string key = $"{fontFamily}|{fontSize:0.##}|{fontStyle}";

            if (fontCache.TryGetValue(key, out Font? cachedFont))
                return cachedFont;

            var font = new Font(fontFamily, fontSize, fontStyle);
            fontCache[key] = font;
            return font;
        }
        // =====================================================================================================
        public static Label GetLabel(string text, Font font)
        {
            string key = $"{text}|{font.Name}|{font.Size:0.##}|{font.Style}";

            if (labelCache.TryGetValue(key, out Label? cachedLabel))
                return cachedLabel;

            var label = new Label
            {
                Text = text,
                Font = font
            };
            labelCache[key] = label;
            return label;
        }
        // =====================================================================================================
        public static Pen GetPen(Color color, float width)
        {
            string key = $"{color.R}|{color.G}|{color.B}|{width:0.##}";

            if (penCache.TryGetValue(key, out Pen? cachedPen))
                return cachedPen;

            var pen = new Pen(color, width);
            penCache[key] = pen;
            return pen;
        }
        // =====================================================================================================
        public static void Clear()
        {
            foreach (var font in fontCache.Values)
            {
                font.Dispose();
            }
            foreach (var label in labelCache.Values)
            {
                label.Dispose();
            }
            foreach (var pen in penCache.Values)
            {
                pen.Dispose();
            }
            fontCache.Clear();
            labelCache.Clear();
            penCache.Clear();
        }
        // =====================================================================================================
    }
}
