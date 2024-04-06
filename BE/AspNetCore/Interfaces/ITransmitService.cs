namespace PixelPalette.Interfaces
{
    public interface ITransmitService
    {
        void Transmit<A, B>(A model, ref B entity);
    }
}
