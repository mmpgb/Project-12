using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MusicPlaylistAnalyzer

{

    public class Song

    {
        public int Time, Size, Year, Plays;
        public string Name, Album, Artist, Genre;


        public Song(string name, string album, string artist, string genre, int time, int size, int year, int plays)
        {

            Name = name;

            Album = album;

            Artist = artist;

            Genre = genre;

            Time = time;

            Size = size;

            Year = year;

            Plays = plays;


        }

        override public string ToString()

        {

            return String.Format("name: {0}, album: {1}, artist: {2}, genre: {3}, time: {4}, size: {5}, year: {6}, plays: {7}", Name, Album, Artist, Genre, Time, Size, Year, Plays);

        }

    }

    class Program

    {

        static void Main(string[] args)

        {

            string report = null;

            int i;

            List<Song> RowCol = new List<Song>();

            try

            {

                if (File.Exists($"SampleMusicPlaylist.txt") == false)

                {

                    Console.WriteLine("SampleMusicPlaylist text file cannot be opened ");

                }

                else

                {

                    StreamReader sr = new StreamReader($"SampleMusicPlaylist.txt");

                    i = 0;

                    string line = sr.ReadLine();

                    while ((line = sr.ReadLine()) != null)

                    {

                        i = i + 1;

                        try

                        {

                            string[] strings = line.Split('\t');


                            if (strings.Length < 8)

                            {

                                Console.Write("Record Doesnʼt Contain The Correct Number Of Data Elements ");

                                Console.WriteLine($"Row {i} Contains {strings.Length}  values. It Should Contain 8 ");

                                break;
                            }

                            else

                            {

                                Song dataTemp = new Song((strings[0]), (strings[1]), (strings[2]), (strings[3]), Int32.Parse(strings[4]), Int32.Parse(strings[5]), Int32.Parse(strings[6]), Int32.Parse(strings[7]));

                                RowCol.Add(dataTemp);

                            }

                        }

                        catch

                        {
                            Console.Write("Error Occurs Reading Lines From Playlist Data File ");

                            break;
                        }

                    }

                    sr.Close();

                }
            }
            catch (Exception e)

            {
                Console.WriteLine("Playlist Data File Cannot Be Opened ");

                Console.WriteLine("{0}", e);

            }

            try

            {
                Song[] songs = RowCol.ToArray();

                using (StreamWriter write = new StreamWriter("MusicPlaylistReport.txt"))

                {
                    write.WriteLine("Music Playlist Report");

                    var SongsPlays = from song in songs where song.Plays >= 200 select song;

                    report += "Songs That Received 200 Or More Plays: ";

                    foreach (Song song in SongsPlays)
                    {
                        report += song + "\n";
                    }


                    var SongsGenreAlternative = from song in songs where song.Genre == "Alternative" select song;
                    i = 0;
                    foreach (Song song in SongsGenreAlternative)
                    {
                        i++;
                    }
                    report += $"Number Of Songs That Are In The Playlist With The Genre Of “Alternative: {i}”\n";


                    var SongsGenreHipHopRap = from song in songs where song.Genre == "Hip-Hop/Rap" select song;
                    i = 0;
                    foreach (Song song in SongsGenreHipHopRap)
                    {
                        i++;
                    }
                    report += $"Number Of Songs That Are In The PlayList With The Genre Of Hip-Hop/Rap: {i} \n";


                    var SongsAlbumFishbowl = from song in songs where song.Album == "Welcome to the Fishbowl" select song;
                    report += "Songs That Are In The Playlist From The Album Welcome To The Fishbowl: \n";
                    foreach (Song song in SongsAlbumFishbowl)
                    {
                        report += song + "\n";
                    }


                    var Songs1970 = from song in songs where song.Year < 1970 select song;
                    report += "Songs That Are In The Playlist From Before 1970:\n";
                    foreach (Song song in Songs1970)
                    {
                        report += song + "\n";
                    }


                    var Names85Characters = from song in songs where song.Name.Length > 85 select song.Name;
                    report += "Songs That Are More Than 85 Characters Long:\n";
                    foreach (string name in Names85Characters)
                    {
                        report += name + "\n";
                    }


                    var LongestSong = from song in songs orderby song.Time descending select song;
                    report += "Longest Song (In Time):\n";
                    report += LongestSong.First();

                    write.Write(report);

                    write.Close();
                }

                Console.WriteLine("Music Playlist Has Been Created ");
            }

            catch (Exception y)
            {
                Console.WriteLine("Report File Canʼt Be Opened Or Written To ");
                Console.WriteLine("{0}", y);
            }

            Console.ReadLine();
        }
    }
}