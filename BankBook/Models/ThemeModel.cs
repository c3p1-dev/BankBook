using Avalonia.Styling;

namespace BankBook.Models
{
    public class ThemeModel
    {
        public ThemeVariant? AppTheme { get; set; }
        public CustomAccentColorARGB? CustomAccentColor { get; set; }
    }

    public class CustomAccentColorARGB
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
}
