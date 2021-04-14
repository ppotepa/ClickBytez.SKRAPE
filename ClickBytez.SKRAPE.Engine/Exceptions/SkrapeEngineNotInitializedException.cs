using System;
using System.Runtime.Serialization;

namespace ClickBytez.SKRAPE.Engine
{
    [Serializable]
    internal class SkrapeEngineNotInitializedException : Exception
    {
        public SkrapeEngineNotInitializedException()
        {
        }

        public SkrapeEngineNotInitializedException(string message) : base(message)
        {
        }

        public SkrapeEngineNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SkrapeEngineNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}