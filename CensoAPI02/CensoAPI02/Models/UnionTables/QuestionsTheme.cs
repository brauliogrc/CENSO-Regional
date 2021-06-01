using CENSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models.UnionTables
{
    public class QuestionsTheme
    {
        public int QuestionId { get; set; }

        public int ThemeId { get; set; }

        public Question Question { get; set; }

        public Theme Theme { get; set; }
    }
}
