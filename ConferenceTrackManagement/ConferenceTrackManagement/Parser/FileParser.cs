using ConferenceTrackManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConferenceTrackManagement.Parser
{
    public class FileParser : IContentParser
    {
        private string _filePath;
        public FileParser(string context)
        {
            _filePath = context;
        }
        public List<string> GetRawContent()
        {
            try
            {
                return File.ReadAllLines(_filePath).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
