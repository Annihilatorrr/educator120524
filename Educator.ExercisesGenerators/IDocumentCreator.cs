using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educator.ExercisesGenerators
{
    public interface IDocumentCreator
    {
        public byte[] Save(string fileName, string content);
    }
}
