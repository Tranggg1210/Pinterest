using PixelPalette.Interfaces;

namespace PixelPalette.Services
{
    public class TransmitService : ITransmitService
    {
        public void Transmit<A, B>(A model, ref B entity)
        {
            var modelProperties = typeof(A).GetProperties();
            foreach (var modelProperty in modelProperties)
            {
                var value = modelProperty.GetValue(model);
                if (value != null)
                {
                    var entityProperty = entity!.GetType().GetProperty(modelProperty.Name);
                    if (entityProperty != null && entityProperty!.CanWrite)
                        entityProperty!.SetValue(entity, value);
                }
            }
        }
    }
}