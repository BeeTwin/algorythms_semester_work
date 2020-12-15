using System.Collections.Generic;

namespace algorythms_semester_work
{
    public class NamedNode : Node
    {
        public readonly string Name;

        public NamedNode(string name)
        {
            Name = name;
        }

        public override string ToString()
            => Name;
    }
}