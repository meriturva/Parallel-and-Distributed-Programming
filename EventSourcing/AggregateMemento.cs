﻿namespace EventSourcing
{
    internal class AggregateMemento
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
