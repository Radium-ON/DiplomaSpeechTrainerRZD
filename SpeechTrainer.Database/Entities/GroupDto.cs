namespace SpeechTrainer.Database.Entities
{
    public class GroupDto
    {
        public int Id { get; }
        public string GroupName { get; }

        public GroupDto(int id, string groupName)
        {
            Id = id;
            GroupName = groupName;
        }

        public GroupDto()
        {
        }

        #region Overrides of Object

        public override string ToString()
        {
            return GroupName;
        }

        #endregion
    }
}
