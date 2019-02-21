using System.Collections.Generic;

namespace Test70_483.Types
{
    public class TrackStore : List<string>
    {
        public int RemoveArtist(string removeName)
        {
            var removeList = new List<string>();
            foreach (string track in this)
                if (track == removeName)
                    removeList.Add(track);
            foreach (string track in removeList)
                this.Remove(track);
            return removeList.Count;
        }
    }
}