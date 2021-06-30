using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using SpeechTrainer.Core.ModelObservable;

namespace SpeechTrainer.Core.Utills
{
    public sealed class TrainingStartMessage : ValueChangedMessage<SituationObservable>
    {
        public PositionObservable Position { get; }
        public TrainingStartMessage(SituationObservable situation, PositionObservable position) : base(situation)
        {
            Position = position;
        }
    }
}
