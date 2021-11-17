using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.InputModels
{
    public class CommentInputModel
    {
        public string Content { get; private set; }
        public int IdProject { get; private set; }
        public int UserId { get; private set; }
    }
}
