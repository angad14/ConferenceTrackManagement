using System.Collections.Generic;

namespace ConferenceTrackManagement.Interfaces
{
    public interface IContentParser
    {
        List<string> GetRawContent();
    }
}
