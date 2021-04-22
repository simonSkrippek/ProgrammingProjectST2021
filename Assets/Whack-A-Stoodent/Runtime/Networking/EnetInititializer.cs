using ENet;

namespace WhackAStoodent.Runtime.Networking
{
    internal static class EnetInitializer
    {
        private static uint _initializeCounter;
        
        public static bool Initialize()
        {
            if (_initializeCounter == 0U)
            {
                if (Library.Initialize())
                {
                    _initializeCounter = 1U;
                    return true;
                }
            }
            else
            {
                _initializeCounter ++;
                return true;
            }
            return false;
        }
        
        public static void Deinitialize()
        {
            if (_initializeCounter > 0U)
            {
                --_initializeCounter;
                if (_initializeCounter == 0U)
                {
                    Library.Deinitialize();
                }
            }
        }
    }
}