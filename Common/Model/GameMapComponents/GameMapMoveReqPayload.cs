namespace Common.Model.GameMapComponents
{
    public class GameMapMoveReqPayload
    {
        public static byte[] CreatePayload(GameMapAction.MoveAction moveAction)
        {
            byte[] byteArray;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream);
            binaryWriter.Write((int)moveAction);
            byteArray = memoryStream.ToArray();
            binaryWriter.Close();
            memoryStream.Close();
            return byteArray;
        }

        public static void ParsePayload(byte[] payload, out GameMapAction.MoveAction moveAction)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(payload);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(memoryStream);
            if ((binaryReader.ReadBoolean() == true))
            {
                moveAction = (GameMapAction.MoveAction)binaryReader.ReadInt32();
            }
            else
            {
                moveAction = GameMapAction.MoveAction.GoStraight;
            }
            binaryReader.Close();
            memoryStream.Close();
        }
    }
}
