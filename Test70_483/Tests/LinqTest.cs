using System;
using System.Collections.Generic;
using System.Linq;
using Test70_483.Types;

namespace Test70_483.Tests
{
    public class LinqTest
    {
        private List<Artist> _artists = new List<Artist>()
        {
            new Artist(){Id = 5, Name = "Bal"},
            new Artist(){Id = 89, Name = "Chika"}
        };
        private List<MusicTrack> _tracks = new List<MusicTrack>()
        {
            new MusicTrack(){Id = 5,ArtistId = 5, Title = "Bombi nom"},
            new MusicTrack(){Id = 52,ArtistId = 5, Title = "Bombi nom remastered"},
            new MusicTrack(){Id = 89,ArtistId = 89, Title = "Bombi nom stilled"}
        };

        public void Test()
        {
            var groups = _tracks.Join(_artists,
                    track => track.ArtistId,
                    artist => artist.Id,
                    (track, artist) => new {Author = artist.Name, Title = track.Title}).GroupBy(x => x.Author)
                .Select(grp => new {grp.Key, Count = grp.Count()});
            foreach (var @group in groups)
            {
                Console.WriteLine($"{group.Key} has {group.Count} tracks");
            }
        }
    }
}