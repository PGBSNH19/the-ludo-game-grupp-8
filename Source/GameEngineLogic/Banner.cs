using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineLogic
{
    public class Banner
    {
        public static List<string> ListBan(string SelectedColor, List<string> subjects)
        {
            for (int i = 0; i < subjects.Count; i++)
            {
                if (subjects[i] == SelectedColor)
                {
                    subjects.RemoveAt(i);
                    return subjects;
                }
            }
            return subjects;

        }
    }
}
