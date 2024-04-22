using Microsoft.IdentityModel.Tokens;
using PixelPalette.Data;
using PixelPalette.Interfaces;

namespace PixelPalette.Helpers
{
    public class ApplicationTool : ITool
    {
        private readonly PixelPaletteContext _context;
        public ApplicationTool(PixelPaletteContext context)
        {
            _context = context;
        }
        public void Duplicate<A, B>(A root, ref B target)
        {
            var rootProperties = typeof(A).GetProperties();
            foreach (var rootProperty in rootProperties)
            {
                var value = rootProperty.GetValue(root);
                if (!string.IsNullOrEmpty(value?.ToString()))
                {
                    var targetProperty = target!.GetType().GetProperty(rootProperty.Name);
                    if (targetProperty != null && targetProperty!.CanWrite)
                        targetProperty!.SetValue(target, value);
                }
            }
        }
    }
}