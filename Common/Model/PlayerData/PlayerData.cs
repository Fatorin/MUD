using System;
using System.Collections.Generic;
using System.Dynamic;
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
            MapSeed = 0;
            PlayeyFace = PlayerFaceEnum.Front;
            PlayerStatus = PlayerStatusEnum.Normal;
        }
        public int PlayerUid { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }

        public int MapSeed { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public PlayerFaceEnum PlayeyFace { get; set; }
        public PlayerStatusEnum PlayerStatus { get; set; }
        public enum PlayerFaceEnum { Front, Left, Right, Back };

        public enum PlayerStatusEnum { Normal, Dead, Stun, Scald, Frostbite };

        public override string ToString()
        {
            return $"PlayerId={PlayerUid}, Name={Name}, HP={HP}, MP={MP}, Atk={Atk}, Def={Def}, Level={Level}, Exp={Exp}, PosX={PosX}, PosY={PosY}";
        }
    }
}
