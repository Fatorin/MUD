using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.PlayerData
{
    [Serializable]
    public class PlayerData
    {
        public PlayerData(int playerUid)
        {
            PlayerUid = playerUid;
            Name = "None";
            HP = 10;
            MP = 10;
            Atk = 10;
            Def = 10;
            Level = 1;
            Exp = 0;
            PosX = 0;
            PosY = 0;
        }
        public int PlayerUid { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public override string ToString()
        {
            return $"PlayerId={PlayerUid}, Name={Name}, HP={HP}, MP={MP}, Atk={Atk}, Def={Def}, Level={Level}, Exp={Exp}, PosX={PosX}, PosY={PosY}";
        }
    }
}
