//  Copyright (c) 2021 Esper Thomson
//
//  This file is part of KeybindLib.
//
//  KeybindLib is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  KeybindLib is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with KeybindLib.  If not, see <http://www.gnu.org/licenses/>.

using Stdout = System.Console;
using SRMLOut = SRML.Console.Console;

namespace KeybindLib
{
    internal static class Log
    {
        public static void Write(object msg)
        {
            string text = $"{nameof(KeybindLib)}: {msg}";

            Stdout.WriteLine(text);
            SRMLOut.Log(text);
        }

        public static void Append(string text)
        {
            Stdout.Write(text);
            SRMLOut.Log(text);
        }
    }
}
