using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SpeechTrainer.Core.ModelObservable
{
    public class GroupObservable : ObservableValidator
    {
        public int Id { get; }
        public string GroupName { get; }

        public GroupObservable(int id, string groupName)
        {
            Id = id;
            GroupName = groupName;
        }

        #region Overrides of Object

        public override string ToString()
        {
            return GroupName;
        }

        #endregion
    }
}