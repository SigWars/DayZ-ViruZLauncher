//
//  IniFile.cs
//
//  Author:
//      SigWar
//
//  Copyright (c) 2020 ViruZ Standalone
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

// VZUPDATE
namespace ViruZLauncher.Launcher
{
    public class IniFile // revision 11
	{
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFile"/> class.
        /// </summary>
		/// <param name="IniPath"></param>
        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, string.Empty, RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
    /* ************************************************************************
     * ************************************************************************
    // Creates or loads an INI file in the same directory as your executable
    // named EXE.ini (where EXE is the name of your executable)
    var MyIni = new IniFile();

    // Or specify a specific name in the current dir
    var MyIni = new IniFile("Settings.ini");

    // Or specify a specific name in a specific dir
    var MyIni = new IniFile(@"C:\Settings.ini");

    //You can write some values like so:
    MyIni.Write("DefaultVolume", "100");
    MyIni.Write("HomePage", "http://www.google.com");

    //To create a file like this:
    [MyProg]
    DefaultVolume=100
    HomePage=http://www.google.com

    //To read the values out of the INI file:
    var DefaultVolume = IniFile.Read("DefaultVolume");
    var HomePage = IniFile.Read("HomePage");

    //Optionally, you can set [Section]'s:
    MyIni.Write("DefaultVolume", "100", "Audio");
    MyIni.Write("HomePage", "http://www.google.com", "Web");

    //To create a file like this:
    [Audio]
    DefaultVolume=100

    [Web]
    HomePage=http://www.google.com

    //You can also check for the existence of a key like so:

    if(!MyIni.KeyExists("DefaultVolume", "Audio"))
    {
        MyIni.Write("DefaultVolume", "100", "Audio");
    }

    //You can delete a key like so:
    MyIni.DeleteKey("DefaultVolume", "Audio");

    //You can also delete a whole section (including all keys) like so:
    MyIni.DeleteSection("Web");
    */
}
