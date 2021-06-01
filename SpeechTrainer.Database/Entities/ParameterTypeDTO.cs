using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechTrainer.Database.Entities
{
    public class ParameterTypeDto
    {
        public int Id { get; private set; }
        public string TypeName { get; private set; }

        public ParameterTypeDto()
        {
            
        }

        public ParameterTypeDto(int id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }
    }
}
