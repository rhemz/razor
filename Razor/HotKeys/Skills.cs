#region license

// Razor: An Ultima Online Assistant
// Copyright (C) 2020 Razor Development Community on GitHub <https://github.com/markdwags/Razor>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

namespace Assistant.HotKeys
{
    public class SkillHotKeys
    {
        private static HotKeyCallbackState m_Callback;

        private static readonly int[] m_UsableSkills = new int[]
        {
            1, // anatomy
            2, // animal lore
            3, // item identification
            4, // arms lore
            6, // begging
            9, // peacemaking
            12, // cartography
            14, // detect hidden
            15, // Discordance
            16, // evaluate intelligence
            19, // forensic evaluation
            21, // hiding
            22, // provocation
            23, // inscription
            30, // poisoning
            32, // spirit speak
            33, // stealing
            35, // taming
            36, // taste id
            38, // tracking
            46, // Meditation
            47, // Stealth
            48 // RemoveTrap
        };

        public static void Initialize()
        {
            m_Callback = new HotKeyCallbackState(OnHotKey);
            //1044060 = Alchemy in UO cliloc
            foreach (int t in m_UsableSkills)
            {
                HotKey.Add(HKCategory.Skills, (1044060 + t), m_Callback, t);
            }
        }

        private static void OnHotKey(ref object state)
        {
            int sk = (int) state;
            Client.Instance.SendToServer(new UseSkill(sk));
            if (World.Player != null)
            {
                World.Player.LastSkill = sk;
            }

            if (sk == (int) SkillName.Stealth && !World.Player.Visible)
            {
                StealthSteps.Hide();
            }

            SkillTimer.Start();
        }
    }
}