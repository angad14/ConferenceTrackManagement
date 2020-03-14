using ConferenceTrackManagement.BusinessObjects;
using ConferenceTrackManagement.Interfaces;
using System.Collections.Generic;
using ConferenceTrackManagement.Constants;

namespace ConferenceTrackManagement.Parser
{
    public class BaseParser
    {
        IContentParser _contextParser;
        public BaseParser(IContentParser parser)
        {
            _contextParser = parser;
        }

        public List<RawSession> GetInputSet()
        {
            var data = new List<RawSession>();

            var rawData = _contextParser.GetRawContent();

            foreach (var line in rawData)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                    continue;
                var output = GetRawSession(line.Trim());
                data.Add(output);
            }

            return data;
        }
        public RawSession GetRawSession(string val)
        {
            var SessionName = "";
            var SessionDuration = 0;

            var arr = val.Split(' ');

            foreach (var item in arr)
            {
                if (item.EndsWith(TimeConstants.MINUTES) || item == TimeConstants.LIGHTINING)
                {
                    SessionDuration = GetSesstionDuration(item);
                    break;
                }
                SessionName += item + " ";
            }

            return new RawSession() { Name = SessionName.Trim(), Duration = SessionDuration };
        }

        public int GetSesstionDuration(string input)
        {
            if (input == TimeConstants.LIGHTINING)
                return 5;

            var result = "";

            foreach (char item in input)
            {
                if (item > 57 || item < 48)
                    break;

                int.TryParse(item.ToString(), out int val);

                result += item;
            }
            return string.IsNullOrEmpty(result) ? 0 : int.Parse(result);
        }

    }
}
