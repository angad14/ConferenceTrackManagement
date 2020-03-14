using ConferenceTrackManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement.Parser
{
    public class StringParser : IContentParser
    {
        private string _content;
        public StringParser(string context)
        {
            _content = context;
        }
        public List<string> GetRawContent()
        {
            try
            {
                return _content.Split(new[] { Environment.NewLine },
                                                       StringSplitOptions.RemoveEmptyEntries
                                                   ).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
