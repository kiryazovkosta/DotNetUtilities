using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConsoleClient
{
    public class CreateModelDto
    {
        public string Name { get; set; } = null!;
        public int Customer { get; set; }
    }
}
