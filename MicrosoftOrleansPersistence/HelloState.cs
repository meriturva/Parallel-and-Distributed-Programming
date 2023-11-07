using System;

namespace MicrosoftOrleansPersistence
{
    [Serializable]
    public class HelloState
    {
        public string Name { get; set; } = default!;

        public int Counter { get; set; }
    }
}
