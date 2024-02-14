using System;

namespace AssemblyAI
{
    public class MessageType
    {
        public static readonly MessageType FinalTranscript = 
            new MessageType(Value.FinalTranscript, "FinalTranscript");

        public static readonly MessageType SessionBegins = 
            new MessageType(Value.SessionBegins, "SessionBegins");

        public static readonly MessageType PartialTranscript = 
            new MessageType(Value.PartialTranscript, "PartialTranscript");

        public static readonly MessageType SessionTerminated = 
            new MessageType(Value.SessionTerminated, "SessionTerminated");

        private readonly Value _value;
        private readonly String _raw;

        private MessageType(Value value, String raw) {
        this._value = value;
        this._raw = raw;
    }

        public enum Value
        {
            SessionBegins,
            PartialTranscript,
            FinalTranscript,
            SessionTerminated,
            UNKNOWN
        }
    }
}

